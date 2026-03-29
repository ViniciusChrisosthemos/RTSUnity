using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectionView : MonoBehaviour
{
    [Header("Dependecies")]
    [SerializeField] private UnitInteractionManager m_manager;

    [Header("References")]
    [SerializeField] private GameObject m_view;
    [SerializeField] private GameObject m_currentUnitParent;
    [SerializeField] private Image m_imgUnitIcon;
    [SerializeField] private Slider m_sliderUnitProgress;
    [SerializeField] private UIListDisplay m_itemQueueListDisplay;
    [SerializeField] private UIListDisplay m_availableItensListDisplay;

    private ProductionStructure m_productionStructure;

    private void Start()
    {
        m_manager.OnItemsSelected.AddListener(HandleItemSelected);     
        
        m_view.SetActive(false);
    }

    private void HandleItemSelected(List<IInteractable> items)
    {
        if (items.Count == 0)
        {
            m_view.SetActive(false);

            if (m_productionStructure != null)
            {
                m_productionStructure.OnUnitStart.RemoveListener(HandleUnitStarted);
                m_productionStructure.OnUnitProgressUpdated.RemoveListener(HandleProgressChanged);
                m_productionStructure.OnUnitCreated.RemoveListener(HandleUnitCompleted);
            }
        }
        else if (items.Count == 1 && items[0] is ProductionStructure)
        {
            m_view.SetActive(true);

            m_productionStructure = (ProductionStructure)items[0];

            m_productionStructure.OnUnitStart.AddListener(HandleUnitStarted);
            m_productionStructure.OnUnitProgressUpdated.AddListener(HandleProgressChanged);
            m_productionStructure.OnUnitCreated.AddListener(HandleUnitCompleted);

            m_availableItensListDisplay.SetItems(m_productionStructure.AvailableUnits, (controller) => HandleUnitSelected(m_productionStructure, controller.GetItem<AbstractUnitSO>()));
            
            if (m_productionStructure.CurrentUnit == null)
            {
                m_currentUnitParent.SetActive(false);
            }
            else
            {
                m_currentUnitParent.SetActive(true);

                m_imgUnitIcon.sprite = m_productionStructure.CurrentUnit.Icon;

                HandleUnitCompleted(null);
            }
        }
    }

    private void HandleUnitStarted(AbstractUnitSO unit)
    {
        m_currentUnitParent.SetActive(true);
        m_imgUnitIcon.sprite = unit.Icon;
        m_sliderUnitProgress.value = 0f;

        UpdateUnitQueue();
    }

    private void HandleUnitSelected(ProductionStructure productionStructure, AbstractUnitSO unit)
    {
        Debug.Log("Unit In Production");
        productionStructure.AddUnitToProduction(unit);
        UpdateUnitQueue();
    }

    private void HandleProgressChanged(AbstractUnitSO unit, float progress)
    {
        m_sliderUnitProgress.value = progress;
    }

    private void HandleUnitCompleted(AbstractUnitSO unit)
    {
        m_currentUnitParent.SetActive(m_productionStructure.IsWorking);

        UpdateUnitQueue();
    }

    private void UpdateUnitQueue()
    {
        m_itemQueueListDisplay.SetItems(m_productionStructure.Queue, null);
    }
}
