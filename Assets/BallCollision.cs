using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{

    public GameObject ball;

    private const float SPEED_FACTOR = 1f;
    private const float SPIN_FACTOR = 0.2f;

    private AudioSource ballSound;
    

    // Start is called before the first frame update
    void Start()
    {
        ballSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GolfClub"))
        {
            // TODO: Enforce minimum time between strokes

            Debug.Log("Detected a golf ball collision");

            Rigidbody ballRigidBody = GetComponent<Rigidbody>();

            float clubSpeed = other.relativeVelocity.magnitude;
            float forceMagnitude = clubSpeed * SPEED_FACTOR;

            Vector3 forceDir = other.contacts[0].normal;

            ballRigidBody.AddForce(-forceDir * forceMagnitude, ForceMode.Impulse);

            Vector3 spinDir = Vector3.Cross(forceDir, Vector3.up);
            float spinMagnitude = clubSpeed * SPIN_FACTOR;

            ballRigidBody.AddTorque(spinDir * spinMagnitude, ForceMode.Impulse);
        }

        // Play sound for all collisions
        ballSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfHole"))
        {
            Debug.Log("Ball is in the hole!");
            ball.SetActive(false);
            ballSound.Play();
        }
    }
}
