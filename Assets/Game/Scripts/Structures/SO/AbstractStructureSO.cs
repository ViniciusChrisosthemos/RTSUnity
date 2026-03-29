using UnityEngine;

public class AbstractStructureSO : ScriptableObject
{
    [SerializeField] private string m_name;
    [SerializeField] private Sprite m_icon;
    [SerializeField] private Vector2 m_size;
    [SerializeField] private float m_baseHealth;

    public string Name => m_name;
    public Sprite Sprite => m_icon;
    public Vector2 Size => m_size;
    public float BaseHealth => m_baseHealth;
}
