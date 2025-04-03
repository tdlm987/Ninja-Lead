using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehical : Obstacle
{
    //public override void FixedUpdateNetwork()
    //{
    //    base.Move();
    //}
    public virtual void OnCollisionEnter(Collision other)
    {
        RacingBoy racingBoy = other.gameObject.GetComponent<RacingBoy>();
        if( racingBoy != null )
        {
            Debug.Log("Tai nạn rồi!");
            RacingBoy.Instance.CheckHaveMove(false);
            racingBoy.gameObject.SetActive(false);
        }
    }
}
