using UnityEngine;
using System.Collections;

public class RotateStaff : MonoBehaviour
{
    public float bobSpeed = 1f;
    public float bobHeight = 1f;
    public float returnSpeed = 1f;

    private bool isBobbing = false;
    private float originalY;
    private float bobTimer = 0f;
    private float yOffset = 0f;

    private Coroutine returnCoroutine;
    private Transform playerTransform;

    private void Start()
    {
        originalY = transform.position.y;

        // Find the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isBobbing)
        {
            bobTimer += Time.deltaTime;
            float newY = originalY + Mathf.Sin(bobTimer * bobSpeed) * bobHeight;
            transform.position = new Vector3(transform.position.x, newY + yOffset, transform.position.z);

            if (bobTimer >= 1f)
            {
                StopBobbing();
            }
        }
    }

    public void StartBobbing()
    {
        isBobbing = true;
        bobTimer = 0f;
    }

    public void StopBobbing()
    {
        isBobbing = false;
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        returnCoroutine = StartCoroutine(ReturnToOriginalPosition());
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        while (transform.position.y > originalY)
        {
            float newY = Mathf.MoveTowards(transform.position.y, originalY, returnSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY + yOffset, transform.position.z);
            yield return null;
        }
    }

    private void LateUpdate()
    {
        // Adjust the yOffset based on the player's position difference
        yOffset = playerTransform.position.y - originalY;
    }
}
