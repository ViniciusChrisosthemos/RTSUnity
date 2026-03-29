using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UnitInteractionManager : Singleton<UnitInteractionManager>
{
    [SerializeField] private Camera m_mainCamera;

    [SerializeField] private LayerMask m_floorLayer;
    [SerializeField] private LayerMask m_unitLayer;
    [SerializeField] private LayerMask m_structureLayer;
    [SerializeField] private List<FactionSO> m_controlableFactions;

    [Header("Events")]
    public UnityEvent OnSelectionBoxStarted;
    public UnityEvent OnSelectionBoxEnded;
    public UnityEvent<Vector2, Vector2> OnSelectionBoxChanged;
    public UnityEvent<List<IInteractable>> OnItemsSelected;

    private bool m_selectionBoxIsActive = false;
    private Vector2 m_selectionBoxStartPosition;

    private List<IInteractable> m_currentSelection = new();

    private void Start()
    {
        InputManager.Instance.OnInputReceived += HandleInputReceived;
    }

    private void Update()
    {
        if (m_selectionBoxIsActive)
        {
            var mousePosition = InputManager.Instance.MousePosition;

            OnSelectionBoxChanged?.Invoke(m_selectionBoxStartPosition, mousePosition);
        }
    }

    private void HandleInputReceived(InputType inputType, InputArg inputArg)
    {
        switch (inputType)
        {
            case InputType.Mouse_Left_Pressed: HandleLeftClickPressed(inputArg.ReadValue<Vector2>()); break;
            case InputType.Mouse_Left_Released: HandleLeftClickReleased(inputArg.ReadValue<Vector2>()); break;
            case InputType.Mouse_Right_Pressed: HandleRightClickPressed(inputArg.ReadValue<Vector2>()); break;
            default: break;
        }
    }

    private void HandleLeftClickPressed(Vector2 mousePosition)
    {
        m_selectionBoxStartPosition = mousePosition;

        m_selectionBoxIsActive = true;

        OnSelectionBoxStarted?.Invoke();
    }

    private void HandleLeftClickReleased(Vector2 mousePosition)
    {
        m_currentSelection.ForEach(interactable => interactable.Deselect());
        m_currentSelection.Clear();

        if (Vector3.Distance(m_selectionBoxStartPosition, mousePosition) >= 0.1)
        {
            Ray startRay = m_mainCamera.ScreenPointToRay(m_selectionBoxStartPosition);
            Ray endRay = m_mainCamera.ScreenPointToRay(mousePosition);

            Physics.Raycast(startRay, out RaycastHit startHitInfo, 1000, m_floorLayer);
            Physics.Raycast(endRay, out RaycastHit endHitInfo, 1000, m_floorLayer);

            var (center, radius) = GeometryHelper.GetSphere(startHitInfo.point, endHitInfo.point);

            var colliders = Physics.OverlapSphere(center, radius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable controller))
                {
                    if (!controller.IsActive()) continue;

                    var pos2D = m_mainCamera.WorldToScreenPoint(collider.transform.position);
                    
                    if (GeometryHelper.IsPointInsideRect(m_selectionBoxStartPosition, mousePosition, pos2D))
                    {
                        m_currentSelection.Add(controller);
                    }
                }
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = m_mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    m_currentSelection.Add(interactable);
                }
            }
        }

        m_selectionBoxIsActive = false;
        m_currentSelection.ForEach(interactable => interactable.Select());

        OnSelectionBoxEnded?.Invoke();
        OnItemsSelected?.Invoke(m_currentSelection);
    }

    private void HandleRightClickPressed(Vector2 mousePosition)
    {
        var notAllyFactionUnits = m_currentSelection.Where(interactable => !m_controlableFactions.Contains(interactable.GetFaction())).ToList();

        foreach (var unit in notAllyFactionUnits)
        {
            var index = m_currentSelection.IndexOf(unit);

            m_currentSelection[index].Deselect();
            m_currentSelection.RemoveAt(index);
        }

        if (m_currentSelection.Count == 0) return;

        var ray = m_mainCamera.ScreenPointToRay(mousePosition);

        // Check for Unit Target
        if (Physics.Raycast(ray, out RaycastHit unitHitInfo, 1000, m_unitLayer))
        {
            if (unitHitInfo.collider.gameObject.TryGetComponent(out IInteractable unit))
            {
                m_currentSelection.ForEach(selectedUnit => selectedUnit.HandleCommand(new UnitTargetCommand(unit)));
            }
        }// Check for Location
        else if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, m_floorLayer))
        {
            var targetPosition = hitInfo.point;

            var locations = GroupMovementHelper.GetLocations(targetPosition, m_currentSelection.Count);

            for (int i = 0; locations.Count > i; i++)
            {
                m_currentSelection[i].HandleCommand(new LocationCommand(locations[i]));
            }
        }
    }

}
