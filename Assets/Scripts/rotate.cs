using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;  // Adjust rotation speed

    void Update()
    {
        // Rotate the object around its local axis (center) continuously
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
