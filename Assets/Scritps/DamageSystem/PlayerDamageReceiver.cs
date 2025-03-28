using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    protected override void Dead()
    {
        base.Dead();
        Debug.Log("Bạn đã chết!");
        Quest.Instance.OnGameOver();
        PlayerMovement.Instance.CheckMove(false);
    }
}
