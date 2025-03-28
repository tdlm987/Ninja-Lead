using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMeasure : TrungMonoBehaviour
{
    private static DistanceMeasure instance;
    public static DistanceMeasure Instance { get => instance; }
    private float distance = 0;
    public float Distance { get { return distance; } }
    private float distanceTravelled = 0f;
    private Vector3 lastPosition;
    private bool isMeasureDistance = true;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(DistanceMeasure.instance == null)
            DistanceMeasure.instance = this;
    }
    protected override void Start()
    {
        base.Start();
        this.LoadDistanceUI();
    }
    private void LoadDistanceUI()
    {   
        lastPosition = PlayerMovement.Instance.transform.position;
        Quest.Instance.DisplayCurrentDistance(this.distance);
    }
    private void Update()
    {
        this.MeasureDistanceUI();
    }
    private void MeasureDistanceUI()
    {
        if (!this.isMeasureDistance) return;
        float distanceThisFrame = Vector3.Distance(PlayerMovement.Instance.transform.position, lastPosition);
        distanceTravelled += distanceThisFrame;
        lastPosition = PlayerMovement.Instance.transform.position;
        this.distance = distanceTravelled;
        Quest.Instance.DisplayCurrentDistance(this.distance);
    }
    public virtual void UpdateStateMeasureDistance(bool _isMeasure)
    {
        this.isMeasureDistance = _isMeasure;
    }
}
