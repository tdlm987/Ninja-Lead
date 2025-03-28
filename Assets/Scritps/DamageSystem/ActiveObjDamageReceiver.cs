using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjDamageReceiver : DamageReceiver
{
    [SerializeField] protected RacingBoy racingBoy;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRacingBoy();
    }
    protected virtual void LoadRacingBoy()
    {
        if (this.racingBoy != null) return;
        this.racingBoy = GetComponentInParent<RacingBoy>();
        Debug.Log(transform.name + "LoadRacingBoy:",gameObject);
    }
    protected override void Dead()
    {
        base.Dead();
        Debug.Log("Tai nạn!");  
    }
}
