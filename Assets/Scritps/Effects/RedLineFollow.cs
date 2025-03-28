using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RedLineFollow : TrungMonoBehaviour
{
    [SerializeField] protected float lineWidth;


    [Header("Use for Effect FX")]
    [SerializeField] protected bool isEffectFX;
    [SerializeField] protected float timeLoop;
    [SerializeField] protected float timeRoutine;
    [SerializeField] protected float lifeTime;

    [SerializeField] private Transform positionTarget;
    [SerializeField] private Vector3 offSet;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.LoadState();
        this.LineEffectFX();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.LoadState();
    }
    protected virtual void LoadState()
    {
        this.isEffectFX = false;
        if(this.positionTarget != null ) transform.position = this.positionTarget.position + offSet;
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScale();
    }
    protected override void Start()
    {
        base.Start();
    }
    private void LoadScale()
    {
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,this.lineWidth);
    }
    protected virtual void LineEffectFX()
    {
        StartCoroutine(this.LineRoutine());
    }
    protected IEnumerator LineRoutine()
    {
        yield return new WaitUntil(() => isEffectFX);
        StartCoroutine(LineEffectFXRoutine());
    }
    protected IEnumerator LineEffectFXRoutine()
    {
        int dem = 0;
        while (dem < timeLoop)
        {
            yield return new WaitForSeconds(timeRoutine);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(timeRoutine);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            dem++;
        }
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
    public virtual void IsActiveEffectFX(bool _isActive)
    {
        this.isEffectFX = _isActive;
    }
}
