using UnityEngine;

public abstract class AbstractBaseStructure : MonoBehaviour, IInteractable
{
    public string Name { get; private set; }
    public Vector2 Size { get; private set; }
    public HealthController HealthController { get; private set; }
    public FactionSO Faction { get; private set; }

    public void Deselect()
    {

    }

    public FactionSO GetFaction()
    {
        return Faction;
    }

    public void HandleCommand(UnitCommand command)
    {

    }

    public bool IsActive()
    {
        return HealthController.IsAlive();
    }

    public void Select()
    {
        Debug.Log($"{name} selecionado!");
    }

    public virtual void Setup(AbstractStructureSO structureSO, FactionSO faction)
    {
        Faction = faction;

        Name = structureSO.Name;
        Size = structureSO.Size;
        HealthController = new HealthController(structureSO.BaseHealth, null);
    }
}
