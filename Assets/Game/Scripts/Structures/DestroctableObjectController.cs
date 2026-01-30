using UnityEngine;
using UnityEngine.EventSystems;

public class DestroctableObjectController : MonoBehaviour, ITarget
{
    [SerializeField] private int _health = 70;


    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public void KillTarget()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            KillTarget();
        }
    }

    public void SelectTarget()
    {

    }
    public void DeselectTarget()
    {

    }
}
