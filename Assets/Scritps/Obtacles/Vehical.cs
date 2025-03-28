using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehical : Obstacle
{
    private void Update()
    {
        base.Move();
    }
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
