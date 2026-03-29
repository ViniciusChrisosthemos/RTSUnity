using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnitSO : ScriptableObject, IHasIcon
{
    [Header("Unit Parameters")]
    [SerializeField] private string m_id;
    [SerializeField] private string m_name;
    [SerializeField] private Sprite m_icon;
    [SerializeField] private int m_timeToCreate;
    [SerializeField] private List<ItemHolder<Resources>> m_cost;

    [Header("Base Stats")]
    [SerializeField] private float m_baseHealth;
    [SerializeField] private float m_baseMoveSpeed;
    [SerializeField] private float m_basePower;
    [SerializeField] private float m_baseDefense;
    [SerializeField] private float m_baseAttackSpeed;
    [SerializeField] private float m_baseRange;

    public string ID => m_id;
    public string Name => m_name;
    public Sprite Icon { get => m_icon; }
    public int TimeToCreate => m_timeToCreate;
    public List<ItemHolder<Resources>> Cost => m_cost;
}
