using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;

    private void Start()
    {
        virtualCam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public void SetFollowTarget(Transform target)
    {
        virtualCam.Follow = target;
        virtualCam.LookAt = target;
    }
}
