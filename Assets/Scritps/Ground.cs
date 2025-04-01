using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : PoolObj
{
    [SerializeField] private MapTrigger mapTrigger;
    protected override void OnEnable()
    {
        base.OnEnable();
        if(mapTrigger != null) mapTrigger.canSpawnMap = false;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    public override string GetName()
    {
        return "Level";
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapTrigger();
    }
    protected virtual void LoadMapTrigger()
    {
        if (mapTrigger != null) { return; }
        mapTrigger = FindAnyObjectByType<MapTrigger>();
    }
    private void OnCollisionEnter(Collision other)
    {
        ObjectBase obj = other.gameObject.GetComponent<ObjectBase>();
        if (obj != null)
        {
            obj.CheckMove(obj != null);
        }
    }
}
