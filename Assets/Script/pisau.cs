using System.Collections;
using UnityEngine;
using TMPro;

public class pisau : MonoBehaviour
{
    private short potong = 0;
    private Camera cam;
    private Vector3 posisiAwal;
    private GameObject bahan;
    private DragAndDrop dragDrop;
    public TMP_Text tekspotong;

    private void Start()
    {
        cam = Camera.main;
        posisiAwal = transform.position;
    }

    public void setBahan(GameObject bahan)
    {
        this.bahan = bahan;
        dragDrop = bahan.GetComponent<DragAndDrop>();
    }

    private void OnMouseDown()
    {
        Debug.Log("down");
        potong++;
        StartCoroutine(AnimateMovement());
        tekspotong.text = "Klik Pisau " + (5-potong) + " kali lagi";
        if(potong == 5)
        {
            tekspotong.text = "Bahan memotong";
        }
    }

    private IEnumerator AnimateMovement()
    {
        float duration = 0.5f; // Duration of each movement
        Vector3 originalPosition = transform.position;

        // Move down
        Vector3 targetPosition = originalPosition + Vector3.down * 6;
        yield return MoveToPosition(targetPosition, duration);
        if (potong == 5)
        {
            dragDrop.potongBahan();
        }

        // Move up
        targetPosition = originalPosition;
        yield return MoveToPosition(targetPosition, duration);

        if (potong == 5)
        {
            targetPosition = posisiAwal;
            yield return MoveToPosition(targetPosition, duration);
            cam.transform.position = new Vector3(0, 0, -10);
            dragDrop.setAwal();
            potong = 0;
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
