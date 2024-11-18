using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject ball;
    public GameObject fireworks; // Reference to the fireworks GameObject
    public GameObject HUD;

    private const float SPEED_FACTOR = 1.5f;
    private const float SPIN_FACTOR = 0.2f;

    private AudioSource ballSound;

    // Start is called before the first frame update
    void Start()
    {
        ballSound = GetComponent<AudioSource>();
        if (fireworks != null)
        {
            fireworks.SetActive(false); // Ensure fireworks are initially inactive
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GolfClub"))
        {
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

            // Activate fireworks
            if (fireworks != null)
            {
                fireworks.SetActive(true); // Activate the fireworks GameObject
            }
            var hudScript = HUD.GetComponent<HUDScript>();
            hudScript.victory = 1;


            // Disable the ball and play sound
            ballSound.Play();
            ball.SetActive(false);
            
        }
    }
}
