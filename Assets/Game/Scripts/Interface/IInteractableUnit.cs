using UnityEngine;

public interface IInteractableUnit
{
    void Select();
    void Deselect();
    void MoveTo(Vector3 position);
}
