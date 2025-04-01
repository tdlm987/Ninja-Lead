using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null){
            if(player.HasInputAuthority)
            {
                Debug.Log("Bạn đã nhặt được coins!");
                this.UpdateCoinUI(player.GetComponent<PlayerInfo>());
            }
            this.gameObject.SetActive(false);
        }
    }
    private void UpdateCoinUI(PlayerInfo player){
        Quest.Instance.UpdatePlayerUI(player.PlayerID, DefaultValue());
    }
    private int DefaultValue() => this.amountReceive;
}
