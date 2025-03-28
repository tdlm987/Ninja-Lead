using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDespawn : Despawn<Ground>
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            HideThisLevel();
        }
    }
    private void HideThisLevel()
    {
        PoolObj groundParent = GetComponentInParent<PoolObj>();
        groundParent.gameObject.SetActive(false);
    }
}
