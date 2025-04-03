using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class AutoSlider : NetworkBehaviour
{
    [SerializeField] private Slider boostSlider;
    public float increaseSpeed = 1f; // Tốc độ tăng giá trị
    private bool isResetting = false;
    private bool isFulled = false;
    [SerializeField] private float delayTime;
    void Start()
    {
        if (boostSlider == null)
        {
            Debug.LogError("Chưa gán Slider!");
            return;
        }

        boostSlider.value = 0;
        StartCoroutine(IncreaseSlider());
    }

    public override void FixedUpdateNetwork()
    {
        if (HasInputAuthority && !isResetting && IsFulled)
        {
            StartCoroutine(ResetSlider());
        }
    }
    [SerializeField] private Color colorWhenFillFull;
    [SerializeField] private Color colorWhenFillNotFull;

    public bool IsFulled { get => isFulled; }

    IEnumerator IncreaseSlider()
    {
        while (true)
        {
            if (!isResetting)
            {
                boostSlider.value += increaseSpeed * Time.deltaTime;
                boostSlider.value = Mathf.Clamp(boostSlider.value, 0, boostSlider.maxValue);
                ActiveColorWhenFillNotFull();
                if (boostSlider.value == boostSlider.maxValue)
                {
                    this.isFulled = true;
                    break;
                }
            }
            yield return new WaitForSeconds(delayTime);
        }
        ActiveColorWhenFillFull();
    }

    IEnumerator ResetSlider()
    {
        isResetting = true;
        while (boostSlider.value > 0)
        {
            boostSlider.value -= increaseSpeed * 3 * Time.deltaTime; // Tốc độ giảm nhanh hơn
            yield return null;
        }
        boostSlider.value = 0;
        isResetting = false;
        this.isFulled = false;
        StartCoroutine(IncreaseSlider());
    }
    protected virtual void ActiveColorWhenFillFull()
    {
        Transform fill = GetComponentInChildren<Slider>().transform.GetChild(1);
        fill.GetComponent<Image>().color = colorWhenFillFull;
    }
    protected virtual void ActiveColorWhenFillNotFull()
    {
        Transform fill = GetComponentInChildren<Slider>().transform.GetChild(1);
        fill.GetComponent<Image>().color = colorWhenFillNotFull;
    }
}
