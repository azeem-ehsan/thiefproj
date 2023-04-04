using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public RectTransform JoyStickObj;
    public RectTransform Knob;

    void Awake ()
    {
        JoyStickObj = GetComponent<RectTransform>();
    }
}
