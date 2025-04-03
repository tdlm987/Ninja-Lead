using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : ObjectBase
{
    private static PlayerMovement instance;
    public static PlayerMovement Instance { get => instance; }

    [Header("Player")]
    [SerializeField] protected float turnDuration;
    [SerializeField] protected float angleTurn;
    [SerializeField] protected float limitLeftX;
    [SerializeField] protected float limitRightX;
    [SerializeField] private bool isFly = false;

    [Header("Boost")]
    [SerializeField] protected bool isBoost = false;
    [SerializeField] protected float boostAmount = 5f;
    [SerializeField] protected float boostDuration = 5f;
    public bool IsBoost { get => isBoost; }


    protected InputManager inputManager;

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    protected override void LoadState()
    {
        base.LoadState();
        this.isFly = false;
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        instance = this;
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //this.Boost();
        //this.Move();
        //this.Fly();
    }
    public override void FixedUpdateNetwork()
    {
        this.Boost();
        this.Move();
        this.Turn();
        //this.Fly();
    }
    protected override void Move()
    {
        base.Move();
        if (Object.HasInputAuthority)  // Chỉ cập nhật cho Player Local
        {
            transform.position += transform.forward * Runner.DeltaTime * moveSpeed; // Di chuyển (Demo)
        }
    }
    protected override void UpdateForwardSpeed()
    {
        base.UpdateForwardSpeed();
    }
    protected virtual void Turn()
    {
        if (!canMove) return;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (this != null && this.transform != null)
        {
            transform.DORotate(Vector3.up * angleTurn * horizontalInput, turnDuration);
        }
        if (horizontalInput != 0 && Object.HasInputAuthority)
        {
            RPC_TurnSFX();
        }
    }
    [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
    public virtual void RPC_TurnSFX()
    {
        AudioManager.instance.PlaySFX(3, transform);
        Debug.Log("Phát âm thanh!");
    }

    //Fly
    protected IEnumerator FlyRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => this.isFly);
            Fly();
            this.isFly = false;
        }
    }
    protected virtual void Fly()
    {
        
    }
    public virtual void CheckIsFly(bool _isFly)
    {
        this.isFly = _isFly;
    }

    //Boost
    protected virtual void Boost()
    {
        if (!HasInputAuthority) return;
        if (Input.GetKeyDown(KeyCode.B))
            BoostSpeed();
    }
    protected virtual void BoostSpeed()
    {
        if (!IsBoost)
            StartCoroutine(BoostRoutine());
    }
    protected IEnumerator BoostRoutine()
    {
        isBoost = true;
        Obstacle[] obstacles = FindObjectsByType<Obstacle>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].GetComponent<Rigidbody>().isKinematic = true;
            obstacles[i].GetComponent<Collider>().enabled = false;
        }
        float originalSpeed = moveSpeed;
        moveSpeed = Mathf.Min(moveSpeed + boostAmount, maxMoveSpeed);
        yield return new WaitForSeconds(boostDuration);
        // Sau khi trở lại bình thường sẽ tạo một lực đẩy lên các chướng ngại vật xung quanh
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].GetComponent<Rigidbody>().isKinematic = false;
            obstacles[i].GetComponent<Collider>().enabled = true;
        }
        moveSpeed = originalSpeed;
        isBoost = false;
    }
}
