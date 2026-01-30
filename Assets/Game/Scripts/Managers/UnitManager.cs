using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] private Camera _mainCamera;

    private UnitController _selectedCharacter;

    public UnityEvent<UnitController> OnUnitSelected;
    public UnityEvent OnUnitDeselected;

    private Vector3 _startPointSelection;

    private List<ITarget> _currentSelection;

    private void Awake()
    {
        _currentSelection = new List<ITarget>();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            DeselectUnit();
        }

        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (_selectedCharacter == null) return;

        if (!_selectedCharacter.IsAlive())
        {
            DeselectUnit();
            return;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            var mousePosition = Mouse.current.position.ReadValue();

            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.TryGetComponent(out ITarget target))
                {
                    AttackUnit(target);
                }
                else
                {
                    Vector3 targetPosition = hitInfo.point;
                    _selectedCharacter.MoveTo(targetPosition);
                }
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleLeftClickPressed();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            HandleLeftClickRelease();
        }   
    }

    private void HandleLeftClickPressed()
    {


        var mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            _startPointSelection = hitInfo.point;
        }

    }

    private void HandleLeftClickRelease()
    {
        var mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            var endPointSelection = hitInfo.point;

            var center = (_startPointSelection + endPointSelection) / 2;
            var halfsize = new Vector3(Mathf.Abs(endPointSelection.x - _startPointSelection.x), 5f, Mathf.Abs(endPointSelection.z - _startPointSelection.z)) / 2;

            var colliders = Physics.OverlapBox(center, halfsize);
            _currentSelection.ForEach(unit => unit.DeselectTarget());
            _currentSelection.Clear();

            Debug.Log($"Select {colliders.Length}");

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out UnitController controller))
                {
                    SelectUnit(controller);

                    _currentSelection.Add(controller);
                }
            }
        }
    }

    private void AttackUnit(ITarget target)
    {
        _selectedCharacter.AttackTarget(target);
    }

    private void DeselectUnit()
    {
        _selectedCharacter = null;
    }

    public void SelectUnit(UnitController controller)
    {
        _selectedCharacter = controller;
    }
}
