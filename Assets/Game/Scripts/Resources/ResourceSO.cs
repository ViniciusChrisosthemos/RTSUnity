using UnityEngine;

[CreateAssetMenu(fileName = "Resource_", menuName = "ScriptableObjects/RTS/Resource")]
public class ResourceSO : ScriptableObject
{
    [SerializeField] private string m_name;
    [SerializeField] private Sprite m_icon;

    public string Name => m_name;
    public Sprite Icon => m_icon;
}
