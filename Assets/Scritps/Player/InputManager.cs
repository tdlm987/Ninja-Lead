using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)] //Với giá trị bằng -1 thì script này sẽ chạy trước các script có DefaultExecution bằng 0
public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get => instance; }

    public delegate void TouchEvent(Vector2 position, float time);
    public event TouchEvent OnStartTouch;
    public event TouchEvent OnEndTouch;
    public event TouchEvent OnHoldTouch;

    private Vector2 currentDirection = Vector2.zero;

    private PlayerControl touchControls;
    private void Awake()
    {
        touchControls = new PlayerControl();
        instance = this;
    }
    private void OnEnable()
    {
        touchControls.Enable();
        TouchSimulation.Enable();

        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void OnDisable()
    {
        touchControls.Disable();
        TouchSimulation.Disable();

        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);


    }
    private void StartTouch(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        Debug.Log("Touch started " + touchPosition + " with Screen: " + Screen.width);
        currentDirection = GetDirectionFromPosition(touchPosition);
        OnStartTouch?.Invoke(currentDirection, (float)context.startTime);
        //Debug.Log("Touch started " + currentDirection +" with Screen: "+ Screen.width);
    }
    private void EndTouch(InputAction.CallbackContext context)
    {
        //Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        //Vector2 direction = GetDirectionFromPosition(touchPosition);
        currentDirection = Vector2.zero;
        OnEndTouch?.Invoke(currentDirection, (float)context.time);
        Debug.Log("Touch ended " + currentDirection);
    }
    //private void FingerDown(Finger finger)
    //{
    //    if (OnStartTouch != null) OnStartTouch(finger.screenPosition, Time.time);
    //}
    private Vector2 GetDirectionFromPosition(Vector2 position)
    {
        if(position.x == 0) return Vector2.zero;
        return position.x < ((float)(Screen.width) / 2) ? new Vector2(-1, 0) : new Vector2(1, 0);
    }
    private void Update()
    {
        //Debug.Log(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches);
        //foreach (UnityEngine.InputSystem.EnhancedTouch.Touch touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        //{
        //    Debug.Log(touch.phase == UnityEngine.InputSystem.TouchPhase.Began);
        //}
        if (currentDirection != Vector2.zero)
        {
            OnHoldTouch?.Invoke(currentDirection,Time.time);
        }
    }
}
