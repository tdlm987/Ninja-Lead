using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiplayerCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        // Tìm Player của chính mình
        foreach (var p in FindObjectsOfType<PhotonView>())
        {
            if (p.IsMine) // Chỉ lấy player do chính mình điều khiển
            {
                player = p.transform;
                break;
            }
        }

        if (player != null)
            offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        if (player != null)
            transform.position = Vector3.Lerp(transform.position, player.position + offset, 0.1f);
    }
}
