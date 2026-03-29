using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform m_playerUnitsParent;
    public Transform m_enemyUnitsParent;
    public Transform m_playerStructures;

    public CapitalStructureSO m_capitalStructureSO;

    public FactionSO m_playerFaction;
    public FactionSO m_enemyFaction;

    private void Start()
    {
        var playerStats = new UnitStats(100, 15, 10, 4, 1.4f, 1f);
        m_playerUnitsParent.GetComponentsInChildren<BaseUnitController>().ToList().ForEach(unit => unit.Setup(playerStats, m_playerFaction));


        var enemyStats = new UnitStats(100, 10, 5, 2, 1.4f, 1f);
        m_enemyUnitsParent.GetComponentsInChildren<BaseUnitController>().ToList().ForEach(unit => unit.Setup(enemyStats, m_enemyFaction));

        m_playerStructures.GetComponentsInChildren<AbstractBaseStructure>().ToList().ForEach(structure => structure.Setup(m_capitalStructureSO, m_playerFaction));
    }
}
