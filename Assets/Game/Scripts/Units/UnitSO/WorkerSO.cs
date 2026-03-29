using UnityEngine;

[CreateAssetMenu(fileName = "Unit_Worker", menuName = "ScriptableObjects/RTS/Units/Worker")]
public class WorkerSO : AbstractUnitSO
{
    [Header("Worker Parameters")]
    [SerializeField] private int m_actionsBeforeDie;

    public int ActionBeforeDie => m_actionsBeforeDie;
}
