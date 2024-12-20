using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager script
    public GameObject ball;
    public GameObject club;

    public GameObject door;

    private const float SPEED_FACTOR = 0.1f;
    private const float SPIN_FACTOR = 0.0f;
    private const float MAX_CLUB_SPEED = 0.4f;

    private AudioSource ballSound;

    public AudioSource doorOpenSound = null;

    private bool clubActive = true;
    private const float HIT_DELAY = 2.0f;
    private float nextHitTime = 0.0f;

    private int strokeCount = 0;

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
            strokeCount++;
            Debug.Log("Info: Detected collision with " + ball + "(current stroke count: " + getStrokeCount() + ")");

            gameManager.UpdateStrokeCount();
            
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
            ballSound.Play();

        }

        // Play sound for all collisions
        ballSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfHole"))
        {
            Debug.Log(ball + " is in the hole (" + getStrokeCount() + " strokes)");

            // Door could be null, like on hole 5
            if (door != null)
            {
                Animator doorAnimator = door.GetComponent<Animator>();
                if (doorAnimator == null)
                {
                    Debug.Log("Warning: Animator for " + ball + " is null.");
                }
                else
                {
                    doorAnimator.SetBool("Open", true);
                    doorOpenSound.Play();
                    Debug.Log("Info: Door has been opened.");
                }
            }

            // Disable the ball and play sound
            if (ballSound != null)
            {
                ballSound.Play();
            } else
            {
                Debug.Log("Warning: ballSound for " + ball + " is null.");
            }
            
            if (ball == null)
            {
                Debug.Log("Warning: " + ball + " is a null object!");
            } else
            {
           
                ball.SetActive(false);
                Debug.Log("Info: " + ball + " SetActive to false");
            }
        }
    }

    public int getStrokeCount()
    {
        return strokeCount;
    }
}
