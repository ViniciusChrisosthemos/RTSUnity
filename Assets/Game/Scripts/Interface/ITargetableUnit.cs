using UnityEngine;

public interface ITargetableUnit
{
    Transform GetPositon();
    bool IsAlive();
    void Attack(float damage);
}
