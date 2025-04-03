using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class CameraFollowPlayer : NetworkBehaviour
{


    private Transform target;        // Nhân vật cần theo dõi
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // Khoảng cách so với nhân vật
    [SerializeField] private float followSpeed = 10f; // Tốc độ di chuyển camera
    [SerializeField] private float laneMoveSpeed = 15f; // Tốc độ camera di chuyển ngang khi nhân vật rẽ

    private Vector3 targetPosition;

    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float boostFOV = 75f;
    [SerializeField] private float fovSpeed = 5f;
    private Camera cam;
    public bool isFollowPlayer = false;
    private void Awake()
    {
        
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        LoadCameraForPlayer();
    }

    public void LoadCameraForPlayer()
    {
        // Tìm tất cả PlayerMovement trong scene
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();

        foreach (PlayerMovement player in players)
        {
            // Kiểm tra nếu player này là Player Local (có quyền điều khiển)
            if (player.Object.HasInputAuthority && isFollowPlayer)
            {
                target = player.transform;
                gameObject.SetActive(true); // Bật Camera nếu đúng Player Local
                return;
            }
        }
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (target == null) return;
        if (!isFollowPlayer) return;
        // Thay đổi FOV khi boost
        float targetFOV = (PlayerMovement.Instance.IsBoost) ? boostFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (target == null) return;
        if (!isFollowPlayer) return;
        // Vị trí mong muốn của camera (giữ nguyên X của nhân vật)
        targetPosition = target.position + offset;
        targetPosition.x = Mathf.Lerp(transform.position.x, target.position.x, laneMoveSpeed * Time.deltaTime);

        // Di chuyển camera mượt về vị trí mong muốn
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
