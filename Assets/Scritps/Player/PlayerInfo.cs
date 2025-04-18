using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    [Networked]
 [SerializeField]   protected string playerName { get; set; }=string.Empty;

    private static PlayerInfo instance;
    public static PlayerInfo Instance { get => instance; }
    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    protected virtual void LoadComponents()
    {
        this.LoadInstance();
    }
    protected virtual void Start()
    {
        this.ResetPlayerInfo();
    }
    private void LoadInstance()
    {
        if(instance == null)
            instance = this;
    }
    private void ResetPlayerInfo()
    {
        this.m_current_coins = 0;
    }
    [Networked,OnChangedRender(nameof(UpdateUICoin1))] [SerializeField] private int m_current_coins { get; set; } = 0;
    public int Current_Coins { get { return m_current_coins; } }

    private string _playerID;
    public string PlayerID { get { return _playerID; } set { _playerID = value; } }
    public void UpdateCoin(int _amount)
    {
        if (HasStateAuthority) this.m_current_coins += _amount;
    }
    public void UpdateUICoin1()
    {
        Quest.Instance.DisplayCurrentCoins(this.m_current_coins);
    }
    public override void Spawned()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
    }
}
