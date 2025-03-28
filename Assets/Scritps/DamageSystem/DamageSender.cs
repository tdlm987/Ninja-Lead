using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageSender : TrungMonoBehaviour
{
    [SerializeField] protected int damage = 1000;
    public virtual void OnCollisionEnter(Collision other)
    {
        DamageReceiver damageReceiver = other.gameObject.GetComponent<DamageReceiver>();
        if (damageReceiver != null)
        {
            this.Send(damageReceiver);
            Debug.Log("OnCollisionEnter: " + other.gameObject.name);
        }
    }
    protected virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.Decuct(this.damage);
    }
}
