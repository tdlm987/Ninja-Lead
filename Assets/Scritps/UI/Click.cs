using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    Button[] listButtons;
    void Start()
    {
        listButtons = Resources.FindObjectsOfTypeAll<Button>();
    }
}
