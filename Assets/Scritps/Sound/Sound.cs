using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.GoogleVr;
using UnityEngine;

public class Sound : NetworkBehaviour
{
    public static Sound instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
    private void Rpc_MotorBikeMusicPlay()
    {        
        AudioManager.instance.MotorBikeMusicPlay(transform);
    }
    [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
    private void Rpc_StopMotorBikeMusic()
    {
        AudioManager.instance.StopAudioMotorBike(transform);
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void Rpc_CarCollision()
    {        
        AudioManager.instance.PlaySFX(0,transform);
    }
    [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
    public void Rpc_CoinCollision()
    {
        AudioManager.instance.PlaySFX(1,transform);
    }
    [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
    public void Rpc_VolumeNitro()
    {
        AudioManager.instance.PlaySFX(2,transform);
    }
    public void SoundLocal(int index)
    {
        AudioManager.instance.PlaySFX(index,transform);
    }
}
