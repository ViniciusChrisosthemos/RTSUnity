using UnityEngine;

public interface ITarget
{
    Transform GetTransform();

    void TakeDamage(int damage);

    bool IsAlive();

    void KillTarget();

    void SelectTarget();

    void DeselectTarget();
}
