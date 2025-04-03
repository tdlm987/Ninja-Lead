using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingBoy : Obstacle
{
    private static RacingBoy instance;
    public static RacingBoy Instance { get => instance; }

    [SerializeField] protected float timeStartRace;
    [SerializeField] protected float timeLife;
    [SerializeField] protected float distance;
    [SerializeField] protected bool isRunning;
    public bool IsRunning {  get { return isRunning; } }
    [SerializeField] protected bool isNearPlayer;

    [SerializeField] protected Transform redLineObj;

    protected bool isBoyComing = false;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.Race();
        this.Warning();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.LoadPosition();
    }
    protected override void Awake()
    {
        base.Awake();
    }


    protected override void LoadState()
    {
        base.LoadState();
        Invoke("Delete", 30f);
        this.isBoyComing = false;
        this.isRunning = false;
        this.isNearPlayer = false;
    }
    protected virtual void LoadPosition()
    {
        transform.position = transform.parent.position;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        instance = this;
    }
    protected virtual void Start()
    {
        
    }
    private void FixedUpdate()
    {
        this.Move();
        
    }
    private void Update()
    {
        this.CheckNearPlayer();
    }
    public virtual void OnCollisionEnter(Collision other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Quest.Instance.OnGameOver(player);
            this.ImpactToPlayer();
        }
        if (redLineObj != null && !isBoyComing) this.redLineObj.transform.gameObject.SetActive(true);
    }
    public virtual void ImpactToPlayer()
    {
        Debug.Log("Bạn đã chết!");
        PlayerMovement.Instance.CheckMove(false);
    }
    protected virtual void Race()
    {
        StartCoroutine(this.RacingRoutine());
    }
    private IEnumerator RacingRoutine()
    {
        yield return new WaitUntil (() => canMove);
        yield return new WaitForSeconds(timeStartRace);
        this.isRunning = true;
        yield return new WaitForSeconds(timeLife);
        this.Delete();
    }
    protected override void Move()
    {
        if (!this.isRunning) return;
        base.Move();
    }
    public virtual void CheckHaveMove(bool _isMove)
    {
        this.canMove = _isMove;
        this.isRunning = _isMove;
    }
    protected virtual void CheckNearPlayer()
    {
        this.isNearPlayer = this.isRunning && IsNearPlayer() && IsBehindPlayer();
    }
    protected virtual void Warning()
    {
        StartCoroutine (this.WarningRoutine());
    }
    protected IEnumerator WarningRoutine()
    {
        yield return new WaitUntil(() => this.isNearPlayer);
        isBoyComing = true;
        redLineObj?.GetComponent<RedLineFollow>().IsActiveEffectFX(isBoyComing);
    }
    protected override void Delete()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
    public virtual bool IsNearPlayer()
    {
        return Vector3.Distance(PlayerMovement.Instance.transform.position, this.transform.position) <= distance;
    }
    public virtual bool IsBehindPlayer()
    {
        return PlayerMovement.Instance.transform.position.z > this.transform.position.z;
    }
}
