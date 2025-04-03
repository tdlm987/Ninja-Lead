using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Unity.Mathematics;
using Fusion.Sockets;
using System;

public class GameManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] protected NetworkRunner _runner;
    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected Transform spawnPos;


    [Networked]
    [field: SerializeField] public NetworkDictionary<PlayerRef, PlayerMovement> players => default;
    private void Awake()
    {
        if (!this._runner)
        {
            this._runner = GameObject.FindObjectOfType<NetworkRunner>();
        }

        _runner?.AddCallbacks(this);
    }
    //public void PlayerJoined(PlayerRef player)
    //{

    //    NetworkObject newPlayer = _runner.Spawn(
    //        prefab: playerPrefab,
    //        position: spawnPos.position,
    //        rotation: quaternion.identity,
    //        inputAuthority:player
    //        );
    //}

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log( "newPlayer" );
        if(runner.IsServer)
        {
        NetworkObject newPlayer = runner.Spawn(
            prefab: playerPrefab,
            position: spawnPos.position,
            rotation: quaternion.identity,
            inputAuthority: player
            );
            //   players.Add(player, newPlayer.GetComponent<PlayerMovement>());



            if (player == runner.LocalPlayer)
            {
                CameraFollow cameraManager = FindObjectOfType<CameraFollow>();
                cameraManager.SetFollowTarget(newPlayer.transform);
            }
            Quest.Instance.RegisterPlayerUI(player.PlayerId.ToString());
            newPlayer.GetComponent<PlayerInfo>().PlayerID = player.PlayerId.ToString();
        }

       
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
   
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("Scene load complete!");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }
}
