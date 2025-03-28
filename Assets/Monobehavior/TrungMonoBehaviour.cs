using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrungMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void OnEnable()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void LoadComponents()
    {

    }
    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }
    protected virtual void ResetValue()
    {
        //For override
    }
    protected virtual void OnDisable()
    {

    }
}
