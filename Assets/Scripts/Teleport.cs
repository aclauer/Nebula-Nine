using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportDestination; // Where the player will be teleported
    public GameObject playerCamera;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("GolfClub"))
        {
            Debug.Log("Info: Golf club has entered the collider (" + other + ")");
            // Teleport the player to the destination
            //other.transform.position = teleportDestination.position;
            playerCamera.transform.position = teleportDestination.position;

            // Optional: Reset player velocity if using a Rigidbody
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}
