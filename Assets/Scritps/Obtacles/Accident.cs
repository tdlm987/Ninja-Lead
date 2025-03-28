using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accident : Obstacle
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            this.ImpactToPlayer();
        }
    }
    public virtual void ImpactToPlayer()
    {
        Debug.Log("Bạn đã chết!");
        Quest.Instance.OnGameOver();
        PlayerMovement.Instance.CheckMove(false);
    }
}
