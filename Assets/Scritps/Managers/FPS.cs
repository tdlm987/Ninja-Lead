using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int FPSamount;
    private void Start()
    {
        QualitySettings.vSyncCount = 0; // Tắt VSync để Application.targetFrameRate hoạt động chính xác
        Application.targetFrameRate = FPSamount;
    }

}
