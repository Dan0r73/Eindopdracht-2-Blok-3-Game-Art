using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 50, 0);

    void Update()
    {
        // Rotate the object continuously
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
