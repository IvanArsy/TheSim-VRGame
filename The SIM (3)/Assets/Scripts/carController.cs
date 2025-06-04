using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;


public class carController : MonoBehaviour
{

    public WheelCollider[] wheels = new WheelCollider[4];
    public InputActionReference trigger;
    public InputActionReference triggerForward;
    public InputActionReference triggerBackward;
    public XRKnob knob;
    public bool isPressed = false;
    public float motorTorque;
    public float breakTorque;
    public float steeringMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    [SerializeField] private Transform[] wheelMeshes;


    // Update is called once per frame
    void Update()
    {
        // Handle forward movement
        if (triggerForward.action.IsPressed())
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 0;
                wheels[i].motorTorque = motorTorque; // Positif untuk maju
            }
        }
        // Handle backward movement
        else if (triggerBackward.action.IsPressed())
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 0;
                wheels[i].motorTorque = -motorTorque; // Negatif untuk mundur
            }
        }
        else // Jika tidak ditekan, aktifkan rem
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = 0;
                wheels[i].brakeTorque = breakTorque;
            }
        }

        // Steering menggunakan knob
        for (int i = 0; i < wheels.Length - 2; i++)
        {
            wheels[i].steerAngle = (knob.value - 0.5f) * steeringMax;
        }
    }

}
