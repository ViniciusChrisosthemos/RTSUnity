using UnityEngine;

public interface ITargetableUnit
{
    Transform GetPositon();
    bool IsAlive();
    void TakeDamage(ITargetableUnit source, float damage);
}
