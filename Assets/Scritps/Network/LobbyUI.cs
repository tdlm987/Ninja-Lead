using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] protected NetworkManager manager;


    [SerializeField] public TMP_InputField roomName;

    public virtual void CreateSharedMode()
    {
        string name=roomName.text;
        this.manager.CreateRoom(GameMode.AutoHostOrClient, name);
    }

    public virtual void JoinRoom()
    {
        string name = roomName.text;
        this.manager.JoinRoom(name);
    }    
    public virtual void JoinRandom()
    {
        this.manager.JoinRandomRoom();
    }

}
