using System.Collections;
using UnityEngine;

public class pisau : MonoBehaviour
{
    private short potong = 0;
    private void OnMouseDown()
    {
        Debug.Log("down");
        potong++;
        StartCoroutine(AnimateMovement());
    }

    private IEnumerator AnimateMovement()
    {
        float duration = 0.5f; // Duration of each movement
        Vector3 originalPosition = transform.position;

        // Move down
        Vector3 targetPosition = originalPosition + Vector3.down * 5;
        yield return MoveToPosition(targetPosition, duration);

        // Move up
        targetPosition = originalPosition;
        yield return MoveToPosition(targetPosition, duration);

        // Move left
        targetPosition = originalPosition + Vector3.left * 2;
        yield return MoveToPosition(targetPosition, duration);

        if (potong == 5)
        {
            targetPosition = originalPosition + Vector3.right * 10;
            yield return MoveToPosition(targetPosition, duration);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        transform.position = targetPosition; // Ensure the final position is set
    }
}
