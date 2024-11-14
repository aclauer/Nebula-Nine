using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private const float speedFactor = 1f;
    private const float spinFactor = 1f;

    private float deadTime = 1000f;  // Minimum time between strokes
    private float lastHit = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("GolfClub"))
        {
            float currentTime = (float) DateTime.Now.TimeOfDay.TotalMilliseconds;
            if (currentTime - lastHit > deadTime)
            {
                return;
            }

            lastHit = currentTime;


            Rigidbody ballRigidBody = GetComponent<Rigidbody>();

            float clubSpeed = collider.relativeVelocity.magnitude;
            float forceMagnitude = clubSpeed * speedFactor;

            Vector3 forceDir = collider.contacts[0].normal;

            ballRigidBody.AddForce(-forceDir * forceMagnitude, ForceMode.Impulse);

            Vector3 spinDir = Vector3.Cross(forceDir, Vector3.up);
            float spinMagnitude = clubSpeed * spinFactor;

            ballRigidBody.AddTorque(spinDir * spinMagnitude, ForceMode.Impulse);
        }
    }
}
