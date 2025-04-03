using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Coin : PowerUp
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null && player.Object.HasInputAuthority)
        {
            Debug.Log("Bạn đã nhặt được coins!");
            this.UpdateCoinUI(player.GetComponent<PlayerInfo>());

            // Gửi RPC với NetworkObject của Player
            this.RPC_CoinSFX(player.Object);
        }
        this.gameObject.SetActive(false);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_CoinSFX(NetworkObject playerObj)
    {
        AudioManager.instance.PlaySFX(4, playerObj.transform);
        Debug.Log("Sound Coin!");
    }
    private void UpdateCoinUI(PlayerInfo player){
        Quest.Instance.UpdatePlayerUI(player.PlayerID, DefaultValue());
    }
    private int DefaultValue() => this.amountReceive;
}
