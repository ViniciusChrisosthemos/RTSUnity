using UnityEngine;

public class UISelectionRectView : MonoBehaviour
{
    [SerializeField] private UnitInteractionManager m_unitInteractionManager;

    [SerializeField] private GameObject m_view;
    [SerializeField] private RectTransform m_selectionBox;

    private void Start()
    {
        m_unitInteractionManager.OnSelectionBoxStarted.AddListener(HandleSelectionStarted);
        m_unitInteractionManager.OnSelectionBoxEnded.AddListener(HandleSelectionEnded);
        m_unitInteractionManager.OnSelectionBoxChanged.AddListener(HandleSelectionUpdated);

        HandleSelectionEnded();
    }

    private void HandleSelectionStarted()
    {
        m_view.SetActive(true);
    }

    private void HandleSelectionEnded()
    {
        m_view?.SetActive(false);
    }

    private void HandleSelectionUpdated(Vector2 startPoint, Vector2 endPoint)
    {
        var rect = GeometryHelper.GetRect(startPoint, endPoint);

        m_selectionBox.position = rect.center;
        m_selectionBox.sizeDelta = rect.size;
    }
}
