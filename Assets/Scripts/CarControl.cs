using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControl : MonoBehaviour
{
    public float vertical, horizontal, speed, driveForce, slowingVelFactor, terminalVelocity;
    Rigidbody rigidBody;
    string id;
    public FloatingJoystick controls;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        driveForce = -40f;
        slowingVelFactor = .95f;
        terminalVelocity = 100f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical")+ controls.Vertical;
        horizontal = Input.GetAxis("Horizontal") + controls.Horizontal;


    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        speed = Vector3.Dot(rigidBody.velocity, transform.forward);

        float rotationTorque = (horizontal * 1.5f) - (rigidBody.angularVelocity.y);
        rigidBody.AddRelativeTorque(0f, rotationTorque, 0f, ForceMode.VelocityChange);

        float sidewaysSpeed = Vector3.Dot(rigidBody.velocity, transform.right);
        Vector3 sideFriction = -transform.right * (sidewaysSpeed / Time.fixedDeltaTime);
        rigidBody.AddForce(sideFriction, ForceMode.Acceleration);

        if (vertical <= 0f)
            rigidBody.velocity *= slowingVelFactor;

        float propulsion = driveForce * vertical - (driveForce / terminalVelocity) * Mathf.Clamp(speed, 0f, terminalVelocity);
        rigidBody.AddForce(transform.forward * -propulsion, ForceMode.Acceleration);
    }
}


