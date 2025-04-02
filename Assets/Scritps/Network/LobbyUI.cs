using Fusion;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] protected NetworkManager manager;


    [SerializeField] public TMP_InputField playerName;

    public virtual void CreateSharedMode()
    {
       // string name=playerName.text;
        PlayerPrefs.SetString("PlayerName",playerName.text);
        this.manager.CreateRoom();
    }

    public virtual void JoinRoom()
    {
        string name = playerName.text;
        this.manager.JoinRoom(name);
    }    
    public virtual void JoinRandom()
    {
        this.manager.JoinRandomRoom();
    }

}
