using System;
using UnityEngine;

public interface IDestroyable
{
    bool IsDestroyed { get; }
    event Action<Owner> OnDestroy;
    void Destroy(Owner sourceOwner);
    void Reset();
}

public enum Owner
{
    Player,
    Enemy
}
[RequireComponent(typeof(BoxCollider2D))]
public class Destroyable : MonoBehaviour, IDestroyable 
{
    public event Action<Owner> OnDestroy;

    public bool IsDestroyed { get; private set; }

    [SerializeField]
    private GameObject _explosion;

    public void Reset()
    {
        IsDestroyed = false;
    }

    public void Destroy(Owner damageSourceOwner)
    {
        if (IsDestroyed)
        {
            return;
        }

        Instantiate(_explosion, transform.position, Quaternion.identity).GetComponent<Explosion>().SetOwner(damageSourceOwner);
        IsDestroyed = true;
        OnDestroy?.Invoke(damageSourceOwner);                
    }
}
