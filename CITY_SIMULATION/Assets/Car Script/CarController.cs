using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class CarController : MonoBehaviour
{
    public InputManager im;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float strengthCoefficient = 20000f;
    public float maxTurn = 20f;
    public float brakeStrength;
    void Start()
    {
        im = GetComponent<InputManager>();
    }

    void FixedUpdate()
    {
        foreach (WheelCollider wheel in throttleWheels)
        {
            if (im.brake)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeStrength;
            }
            else
            {
                wheel.motorTorque = strengthCoefficient * Time.deltaTime * im.throttle;
                wheel.brakeTorque = 0f;
            }
        }
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * im.steer;
        }
    }
}
