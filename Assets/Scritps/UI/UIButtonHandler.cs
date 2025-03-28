using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 currentVector = Vector3.zero;

    // Khi người chơi nhấn hoặc giữ nút UI
    public void OnPointerDown(PointerEventData eventData)
    {
        currentVector = new Vector3(0,0, 0); // Bạn có thể đặt giá trị mong muốn
    }

    // Khi người chơi thả tay khỏi nút UI
    public void OnPointerUp(PointerEventData eventData)
    {
        currentVector = Vector3.zero;
    }

    public Vector3 GetCurrentVector()
    {
        return currentVector;
    }
}
