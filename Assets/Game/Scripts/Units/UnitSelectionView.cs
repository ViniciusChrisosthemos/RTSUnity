using UnityEngine;

public class UnitSelectionView : MonoBehaviour
{
    [SerializeField] private GameObject m_selectionView;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        m_selectionView.SetActive(true);
    }

    public void Deselect()
    {
        m_selectionView.SetActive(false);
    }
}
