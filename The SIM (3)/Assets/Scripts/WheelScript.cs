using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class WheelScript : MonoBehaviour
{
    public GameObject obj;
    public XRKnob knob;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = (knob.value - 0.5f) * 60f;
        obj.transform.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}