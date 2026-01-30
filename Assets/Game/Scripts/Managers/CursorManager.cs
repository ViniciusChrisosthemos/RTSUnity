using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    [Header("Paraters")]
    [SerializeField] private Texture2D _normalCursor;
    [SerializeField] private Texture2D _combatCursor;

    private void Start()
    {
        SetNormalCursor();
    }

    public void SetCombatCursor()
    {
        Cursor.SetCursor(_combatCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetNormalCursor()
    {
        Cursor.SetCursor(_normalCursor, Vector2.zero, CursorMode.Auto);
    }
}
