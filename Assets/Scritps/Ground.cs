using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : PoolObj
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    public override string GetName()
    {
        return "Level";
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
