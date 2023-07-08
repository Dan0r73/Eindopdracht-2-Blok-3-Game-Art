using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    public Transform target; // Referentie naar het spelerobject
    public Vector3 offset; // Offset tussen de speler en de camera
    public float followSpeed = 5f; // Snelheid waarmee de camera de speler volgt

    private Vector3 targetPosition; // Doelpositie van de camera

    private void LateUpdate()
    {
        // Bepaal de doelpositie van de camera op basis van de offset en de positie van de speler
        targetPosition = target.position + offset;

        // Beweeg de camera geleidelijk naar de doelpositie met een vertraging
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
