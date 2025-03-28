using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Slove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if(player != null)
        {
            player.CheckIsFly(true);        
        }
    }
}
