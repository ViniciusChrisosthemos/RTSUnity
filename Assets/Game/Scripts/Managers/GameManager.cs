using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform m_playerUnitsParent;
    public List<EnemyUnitController> m_enemyUnits;

    private void Start()
    {
        m_playerUnitsParent.GetComponentsInChildren<PlayerUnitController>().ToList().ForEach(unit => unit.Setup(new UnitStats(100, 15, 10, 4, 0.7f), new Faction("123", "Player")));
    }
}
