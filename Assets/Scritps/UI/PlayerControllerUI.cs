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
    [SerializeField] private Transform panelChat;
    [SerializeField] private TextMeshProUGUI txtCoin;

    private void Awake()
    {
        
    }
    private void Start()
    {
        PlayerID = GetComponent<NetworkObject>().InputAuthority.ToString();
        this.LoadPanels();
        this.LoadButtons();
    }
    protected virtual void LoadPanels()
    {
        if(this.panelChat != null)
            this.panelChat.gameObject.SetActive(false);
    }

    protected virtual void LoadButtons()
    {
        this.btn_Chat.onClick.RemoveAllListeners();
        this.btn_Chat.onClick.AddListener(OnChat);
    }
    public void UpdateUI(/*string playerName, */int coins/*, float health*/)
    {
        //playerNameText.text = playerName;
        //scoreText.text = "Score: " + score;
        //healthBar.value = health;
        PlayerInfo.Instance.UpdateCoin(coins);
        txtCoin.text = PlayerInfo.Instance.Current_Coins.ToString();
    }

    [SerializeField] private Button btn_Chat;
    public virtual void OnChat()
    {
        this.panelChat.gameObject.SetActive(true);
        this.btn_Chat.onClick.RemoveAllListeners();
        this.btn_Chat.onClick.AddListener(OffChat);
    }
    public virtual void OffChat()
    {
        this.btn_Chat.onClick.RemoveAllListeners();
        this.btn_Chat.onClick.AddListener(OnChat);
        this.panelChat.gameObject.SetActive(false);
    }
}
