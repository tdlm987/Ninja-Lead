using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accident : Obstacle
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (player.HasInputAuthority)
            {
                Quest.Instance.OnGameOver(player);
                var camera = player.GetComponentInChildren<CameraFollowPlayer>();
                camera.isFollowPlayer = false;
                this.ImpactToPlayer(player);
            }
        }
    }
    public virtual void ImpactToPlayer(PlayerMovement player)
    {
        Debug.Log("Bạn đã chết!");
        player.CheckMove(false);
    }
}
