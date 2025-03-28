using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ObjectBase
{
    protected Collider _myCollider;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this._myCollider = GetComponent<Collider>();
    }

}
