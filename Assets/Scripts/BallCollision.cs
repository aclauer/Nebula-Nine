using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject ball;
    public GameObject club;

    private const float SPEED_FACTOR = 0.1f;
    private const float SPIN_FACTOR = 0.0f;
    private const float MAX_CLUB_SPEED = 0.4f;

    private AudioSource ballSound;

    private bool clubActive = true;
    private const float HIT_DELAY = 1.0f;
    private float nextHitTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ballSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextHitTime)
        {
            clubActive = true;
            Collider[] colliders = club.GetComponents<Collider>();
            foreach (Collider c in colliders) {
                c.enabled = true;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GolfClub") & clubActive)
        {
            Debug.Log("Detected a golf ball collision");

            Rigidbody ballRigidBody = GetComponent<Rigidbody>();
            float clubSpeed = Math.Max(other.relativeVelocity.magnitude, MAX_CLUB_SPEED);
            float forceMagnitude = clubSpeed * SPEED_FACTOR;

            Vector3 forceDir = other.contacts[0].normal;

            ballRigidBody.AddForce(forceDir * forceMagnitude, ForceMode.Impulse);

            Vector3 spinDir = Vector3.Cross(forceDir, Vector3.up);
            float spinMagnitude = clubSpeed * SPIN_FACTOR;

            ballRigidBody.AddTorque(spinDir * spinMagnitude, ForceMode.Impulse);

            clubActive = false;
            nextHitTime = Time.time + HIT_DELAY;
            
            Collider[] colliders = club.GetComponents<Collider>();
            foreach (Collider c in colliders) {
                c.enabled = false;
            }
        }

        // Play sound for all collisions
        ballSound.Play();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfHole"))
        {
            Debug.Log("Ball is in the hole!");

            // Disable the ball and play sound
            ballSound.Play();
            ball.SetActive(false);
            
        }
    }
}
