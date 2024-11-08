using UnityEngine;

public class OrbitSystem : MonoBehaviour
{
    public Transform earth;       // Earth object
    public Transform moon;        // Moon object
    public Transform earthOrbit;  // Earth orbit center
    public Transform moonOrbit;   // Moon orbit center

    public float earthOrbitSpeed = 10f;  // Speed of Earth's orbit around the Sun
    public float moonOrbitSpeed = 20f;   // Speed of Moon's orbit around the Earth

    void Update()
    {
        // Rotate the Earth Orbit around the Sun's position (the origin)
        earthOrbit.RotateAround(Vector3.zero, Vector3.up, earthOrbitSpeed * Time.deltaTime);

        // Rotate the Moon Orbit around the Earth’s position
        moonOrbit.RotateAround(earth.position, Vector3.up, moonOrbitSpeed * Time.deltaTime);
    }
}
