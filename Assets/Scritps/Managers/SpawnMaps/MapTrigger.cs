﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public bool canSpawnMap = false;
    private void OnTriggerEnter(Collider other)
    {
        //Nếu chạm với người chơi thì ẩn map hiện tại đi, đồng thời sinh ra map thứ hai
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if(player != null && !canSpawnMap)
        {
            canSpawnMap = true;
            SpawnNewMap();
        }
    }
    private void SpawnNewMap()
    {
        GroundSpawner.Instance.SpawnRandomLevel();
    }
}
