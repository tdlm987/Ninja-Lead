using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Fusion;
using TMPro;
using UnityEngine;

public class DistanceMeasure : NetworkBehaviour
{
    private static DistanceMeasure instance;
    public static DistanceMeasure Instance { get => instance; }

    [Networked, OnChangedRender(nameof(DisplayCurrentDistance1))]
    private float distance { get; set; } = 0;
    public float Distance { get { return distance; } }

    public bool IsMeasureDistance { get => isMeasureDistance; set => isMeasureDistance = value; }

    private float distanceTravelled = 0f;
    private Vector3 lastPosition;
    [SerializeField] private bool isMeasureDistance = false;
    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    protected virtual void LoadComponents()
    {
        if(DistanceMeasure.instance == null)
            DistanceMeasure.instance = this;
    }
    protected virtual void Start()
    {
        this.LoadDistanceUI();
    }
    private void LoadDistanceUI()
    {   
        lastPosition = PlayerMovement.Instance.transform.position;
        Quest.Instance.DisplayCurrentDistance(this.distance);
    }
    public void FixedUpdate()
    {
        this.MeasureDistanceUI();
        this.LoadCurrentDistance();
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
    }

    //Tính toán quảng đường cho từng Player
    private void MeasureDistanceUI()
    {
        if (!this.isMeasureDistance) return;
        if (!HasInputAuthority) return;
        if (PlayerMovement.Instance.transform == null) return;
        float distanceThisFrame = Vector3.Distance(PlayerMovement.Instance.transform.position, lastPosition);
        distanceTravelled += distanceThisFrame;
        lastPosition = PlayerMovement.Instance.transform.position;
        DisplayCurrentDistance(distanceTravelled);
        //Quest.Instance.DisplayCurrentDistance(this.distance);
    }
    [SerializeField] private TextMeshProUGUI title_Distance;
    [SerializeField] private TextMeshProUGUI txt_Distance;
    public void DisplayCurrentDistance(float _distance)
    {
        this.title_Distance.text = Localization.title_Distance;
        this.txt_Distance.text = _distance.ToString("N0", new CultureInfo("en-US"));
    }
    public virtual void UpdateStateMeasureDistance(bool _isMeasure)
    {
        this.IsMeasureDistance = _isMeasure;
    }



    //Lưu distance hiện tại để đồng bộ với mạng
    public virtual void LoadCurrentDistance()
    {
        this.distance = distanceTravelled;
    }
    public virtual void DisplayCurrentDistance1()
    {
        //Debug.Log(this.distance);
    }
}
