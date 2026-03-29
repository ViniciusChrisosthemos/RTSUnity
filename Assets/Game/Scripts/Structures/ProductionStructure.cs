using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ProductionStructure : AbstractBaseStructure
{
    public UnityEvent<AbstractUnitSO> OnUnitStart;
    public UnityEvent<AbstractUnitSO> OnUnitCreated;
    public UnityEvent<AbstractUnitSO, float> OnUnitProgressUpdated;

    private uint m_currentID;
    private ItemInProduction m_currentItem;
    private List<ItemInProduction> m_productionQueue;

    private Coroutine m_processOrderCoroutine;

    private void Awake()
    {
        m_currentID = 0;
        m_productionQueue = new List<ItemInProduction>();

        m_processOrderCoroutine = null;
    }

    public uint AddUnitToProduction(AbstractUnitSO unitSO)
    {
        var id = m_currentID++;
        m_productionQueue.Add(new ItemInProduction(id, unitSO));

        UpdateProduction();

        return id;
    }

    public void RmvUnitFromProduction(uint id)
    {
        if (m_currentItem == null) return;

        if (m_currentItem.ID == id)
        {
            m_currentItem = null;

            StopCoroutine(m_processOrderCoroutine);
            m_processOrderCoroutine = null;
        }
        else
        {
            var itemToRemoveIndex = m_productionQueue.FindIndex(item => item.ID == id);

            if (itemToRemoveIndex >= 0)
            {
                m_productionQueue.RemoveAt(itemToRemoveIndex);
            }
        }

        UpdateProduction();
    }

    private void UpdateProduction()
    {
        if (m_processOrderCoroutine != null) return;

        StartNextItem();
    }

    private void HandleOrderCompleted()
    {
        if (m_productionQueue.Count > 0)
        {
            StartNextItem();
        }
        else
        {
            m_processOrderCoroutine = null;
        }

        OnUnitCreated?.Invoke(m_currentItem.Item);
    }

    private void StartNextItem()
    {
        Debug.Log("StartNextItem");
        m_currentItem = m_productionQueue[0];
        m_productionQueue.RemoveAt(0);

        m_processOrderCoroutine = StartCoroutine(ProcessOrderCorotuine(HandleOrderCompleted));
        OnUnitStart?.Invoke(m_currentItem.Item);
    }

    private IEnumerator ProcessOrderCorotuine(Action onFinished)
    {
        var accumTime = 0f;

        while (accumTime < m_currentItem.Item.TimeToCreate)
        {
            accumTime += Time.deltaTime;

            OnUnitProgressUpdated?.Invoke(m_currentItem.Item, accumTime / m_currentItem.Item.TimeToCreate);

            yield return null;
        }

        onFinished?.Invoke();
    }

    public override void Setup(AbstractStructureSO structureSO, FactionSO faction)
    {
        base.Setup(structureSO, faction);

        var captialDataSO = structureSO as CapitalStructureSO;

        AvailableUnits = captialDataSO.AvailableUnits;
    }

    public AbstractUnitSO CurrentUnit => m_currentItem == null ? null : m_currentItem.Item;
    public List<AbstractUnitSO> AvailableUnits { get; private set; }
    public List<AbstractUnitSO> Queue => m_productionQueue.Select(item => item.Item).ToList();
    public bool IsWorking => m_currentItem != null && m_productionQueue.Count != 0;

    private class ItemInProduction
    {
        public uint ID { get; private set; }
        public AbstractUnitSO Item { get; private set; }

        public ItemInProduction(uint id, AbstractUnitSO item)
        {
            ID = id;
            Item = item;
        }
    }
}
