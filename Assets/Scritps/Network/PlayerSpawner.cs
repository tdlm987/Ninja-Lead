using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private Transform PlayerPrefab;
    [SerializeField] private List<Transform> listPositions;
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab.gameObject, new Vector3(Random.Range(listPositions[0].position.x, listPositions[1].position.x), listPositions[0].position.y, listPositions[0].position.z), Quaternion.identity,
                Runner.LocalPlayer, (runner, obj) =>
                {
                    //var _player = obj.GetComponent<PlayerSetup>();
                    //_player.SetupCamera();
                    var camera = FindAnyObjectByType<CameraFollowPlayer>();
                    camera.LoadCameraForPlayer();
                    Quest.Instance.RegisterPlayerUI(player.PlayerId.ToString());
                    obj.GetComponent<PlayerInfo>().PlayerID = player.PlayerId.ToString();
                });
        }
    }
}
