using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null){
            Debug.Log("Bạn đã nhặt được coins!");
            this.UpdateCoinUI();
            this.gameObject.SetActive(false);
        }
    }
    private void UpdateCoinUI(){
        PlayerInfo.Instance.UpdateCoin(DefaultValue());
    }
    private int DefaultValue() => this.amountReceive;
}
