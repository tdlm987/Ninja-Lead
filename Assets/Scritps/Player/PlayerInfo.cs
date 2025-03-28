using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : TrungMonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance { get => instance; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInstance();
    }
    protected override void Start()
    {
        base.Start();
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
    private int m_current_coins = 0;
    public int Current_Coins { get { return m_current_coins; } }
    public void UpdateCoin(int _amount)
    {
        this.m_current_coins += _amount;
        Quest.Instance.DisplayCurrentCoins(this.m_current_coins);
    }
}
