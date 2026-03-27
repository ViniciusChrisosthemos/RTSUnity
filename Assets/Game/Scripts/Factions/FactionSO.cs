using UnityEngine;

[CreateAssetMenu(fileName = "Faction", menuName = "ScriptableObjects/RTS/Faction")]
public class FactionSO : ScriptableObject
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_name;
    
    public string ID => m_id;
    public string Name => m_name;
}
