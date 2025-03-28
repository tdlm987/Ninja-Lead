using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : TrungMonoBehaviour
{
    [SerializeField] protected int maxHP;


    protected int currentHP;
    protected bool isDead = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHealthValue();
    }
    protected virtual void LoadHealthValue()
    {
        this.currentHP = this.maxHP;
    }
    public virtual void Decuct(int damage)
    {
        this.currentHP -= damage;
        this.CheckIsDead();
        if(this.currentHP < 0)
            this.currentHP = 0;
        Debug.Log(currentHP);
    }
    protected virtual bool IsDead()
    {
        this.isDead = currentHP <= 0;
        return isDead;
    }
    protected virtual void CheckIsDead()
    {
        if(this.IsDead())
            this.Dead();
    }
    protected virtual void Dead()
    {
    }
}
