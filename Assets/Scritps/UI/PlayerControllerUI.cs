using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerUI : NetworkBehaviour
{
    public string PlayerID;  // ID của Player này
    //public Text playerNameText;
    //public Text scoreText;
    //public Slider healthBar;
    [SerializeField] private Button btnPause;

    [SerializeField] private TextMeshProUGUI txtCoin;

    private void Start()
    {
        PlayerID = GetComponent<NetworkObject>().InputAuthority.ToString();
    }

    public void UpdateUI(/*string playerName, */int coins/*, float health*/)
    {
        //playerNameText.text = playerName;
        //scoreText.text = "Score: " + score;
        //healthBar.value = health;
        txtCoin.text = coins.ToString();
    }
}
