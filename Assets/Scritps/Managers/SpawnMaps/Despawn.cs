using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn<T> : DespawnBase where T:PoolObj
{
    [SerializeField] protected T parent;
    [SerializeField] protected Spawner<T> spawner;
    //[SerializeField] private float timeLife = 1f;
    //[SerializeField] private float currentTime = 1f;


    protected override void Start()
    {
        base.Start();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawner();
        this.LoadSpawner();
        this.DespawnChecking();
    }
    protected virtual void LoadDespawner()
    {
        if (this.parent != null) return;
        this.parent = transform.GetComponentInParent<T>();
        Debug.Log(transform.name + ": LoaParent",gameObject);
    }
    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GameObject.FindAnyObjectByType<Spawner<T>>();
        Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }
    protected virtual void DespawnChecking()
    {
        //Điều kiện để tắt gameObject
        //this.currentTime -= Time.fixedDeltaTime;
        //if (this.currentTime > 0) return;
        this.spawner.Despawn(this.parent);
        //this.currentTime = this.timeLife;
    }
}
