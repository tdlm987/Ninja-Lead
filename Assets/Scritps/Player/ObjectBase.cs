using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : TrungMonoBehaviour
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

    protected Vector3 moveHorizontalDirection = Vector3.zero;
    protected float horizontalInput;
    protected float elapsedTime = 0f;
    

    protected override void OnEnable()
    {
        base.OnEnable();
        this.LoadState();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.LoadState();
    }

    protected override void Awake()
    {
        base.Awake();   
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        rb = GetComponent<Rigidbody>();
    }
    protected virtual void LoadState()
    {
        this.canMove = false;
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
    protected virtual void Move()
    {
        if (!canMove) return;
        float directionZ = moveMode == MoveMode.Forward ? 1f : -1f;
        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime >= this.timeFaster)
            this.UpdateForwardSpeed();
        Vector3 moveForwardDirection = Vector3.forward * directionZ * moveSpeed * Time.fixedDeltaTime;
        Vector3 moveHorizontalDirection = Vector3.right * horizontalInput * horizontalSpeed * Time.fixedDeltaTime;
        Vector3 moveDirection = moveForwardDirection + moveHorizontalDirection;
        rb.MovePosition(rb.position + moveDirection);
    }
    protected virtual void Delete()
    {
    }
}
