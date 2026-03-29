using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure_Capital", menuName = "ScriptableObjects/RTS/Structures/Capital")]
public class CapitalStructureSO : AbstractStructureSO
{
    [SerializeField] private List<AbstractUnitSO> m_availableUnits;

    public List<AbstractUnitSO> AvailableUnits => m_availableUnits;
}
