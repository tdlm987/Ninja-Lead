using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    //private CinemachineFramingTransposer framingTransposer;
    private void Start()
    {
        //framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    private void Update()
    {
        //float scroll = Input.GetAxis("Mouse ScrollWheel");
        //framingTransposer.m_CameraDistance -= scroll;
        //framingTransposer.m_CameraDistance = Mathf.Clamp(framingTransposer.m_CameraDistance, 2f, 10f);
    }
    public void AssignCamera(Transform playerTransform)
    {
        virtualCamera.Follow = playerTransform;
        virtualCamera.LookAt = playerTransform;
    }
}
