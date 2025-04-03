using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ObjectBase : NetworkBehaviour
{
    [Header("Move")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] public float MoveSpeed { get => moveSpeed; }
    [SerializeField] protected float horizontalSpeed;
    [SerializeField] public float HorizontalSpeed { get => horizontalSpeed; }

    [SerializeField] protected bool canMove;
    [SerializeField] public bool CanMove { get => canMove; set => canMove = value; }
    [SerializeField] protected enum MoveMode { Forward, Backward }
    [SerializeField] protected MoveMode moveMode;

    [SerializeField] protected float maxMoveSpeed;
    [SerializeField] protected float speedIncreaseRate = 0.5f;
    [SerializeField] protected float timeFaster = 30f;

    protected Rigidbody rb;
    private NetworkTransform networkTransform;
    private CharacterController cc;

    protected Vector3 moveHorizontalDirection = Vector3.zero;
    protected float horizontalInput;
    protected float elapsedTime = 0f;
    

    protected virtual void OnEnable()
    {
        this.LoadState();
    }
    protected virtual void OnDisable()
    {
        this.LoadState();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    protected virtual void LoadComponents()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected virtual void LoadState()
    {
        this.canMove = true;
    }
    protected virtual void UpdateForwardSpeed()
    {
        this.elapsedTime = 0f;
        this.moveSpeed = Mathf.Min(this.moveSpeed + this.speedIncreaseRate, this.maxMoveSpeed);
    }
    public virtual void CheckMove(bool _isMove)
    {
        this.canMove = _isMove;
    }
    public override void Spawned()
    {
        // Chỉ enable điều khiển nếu là Player Local
        if (!HasInputAuthority) return;

        // Lấy các component cần thiết
        TryGetComponent(out cc);
        TryGetComponent(out rb);
        TryGetComponent(out networkTransform);
    }
    protected virtual void Move()
    {
        if (!HasInputAuthority && !canMove) return;

        float directionZ = moveMode == MoveMode.Forward ? 1f : -1f;
        elapsedTime += Runner.DeltaTime;

        if (elapsedTime >= timeFaster)
            UpdateForwardSpeed();

        // Tính toán hướng di chuyển
        Vector3 moveForwardDirection = Vector3.forward * directionZ * moveSpeed * Runner.DeltaTime;
        Vector3 moveHorizontalDirection = Vector3.right * horizontalInput * horizontalSpeed * Runner.DeltaTime;
        Vector3 moveDirection = moveForwardDirection + moveHorizontalDirection;

        // Sử dụng NetworkCharacterController nếu có
        if (cc != null)
        {
            cc.Move(moveDirection);
        }
        else if (rb != null)
        {
            if (rb.interpolation != RigidbodyInterpolation.None)
                rb.interpolation = RigidbodyInterpolation.None; // Đảm bảo không bị lag do Interpolation

            rb.MovePosition(rb.position + moveDirection);
        }

        // Đồng bộ vị trí qua NetworkTransform nếu có
        if (networkTransform != null)
        {
            networkTransform.Teleport(transform.position, transform.rotation);
        }

    }
    protected virtual void Delete()
    {
    }
}
