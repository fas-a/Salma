using System.Collections;
using UnityEngine;
using TMPro;

public class ulekan : MonoBehaviour
{
    private short tumbuk = 0;
    private Camera cam;
    private Vector3 posisiAwal;
    private GameObject bahan;
    private DragAndDrop dragDrop;
    public TMP_Text tekspotong;
    public GameObject back;

    private void Start()
    {
        cam = Camera.main;
        posisiAwal = transform.position;
    }

    public void setBahan(GameObject bahan)
    {
        this.bahan = bahan;
        dragDrop = bahan.GetComponent<DragAndDrop>();
        tumbuk = dragDrop.tumbuk;
        back.SetActive(true);
    }

    public void reset()
    {
        back.SetActive(false);
        cam.transform.position = new Vector3(0, 0, -10);
        dragDrop.setAwal();
    }

    private void OnMouseDown()
    {
        Debug.Log("down");
        tumbuk++;
        dragDrop.tumbuk++;
        StartCoroutine(AnimateMovement());
        tekspotong.text = "Klik Ulekan " + (5-dragDrop.tumbuk) + " kali lagi";
        if(dragDrop.tumbuk == 5)
        {
            tekspotong.text = "Bahan Halus";
        }
    }

    private IEnumerator AnimateMovement()
    {
        float duration = 0.5f; // Duration of each movement
        Vector3 originalPosition = transform.position;

        // Move down
        Vector3 targetPosition = originalPosition + Vector3.down * 6;
        yield return MoveToPosition(targetPosition, duration);
        if (dragDrop.tumbuk == 5)
        {
            dragDrop.gepeng();
        }

        // Move up
        targetPosition = originalPosition;
        yield return MoveToPosition(targetPosition, duration);

        if (dragDrop.tumbuk == 5)
        {
            targetPosition = posisiAwal;
            yield return MoveToPosition(targetPosition, duration);
            cam.transform.position = new Vector3(0, 0, -10);
            dragDrop.setAwal();
            tumbuk = 0;
            back.SetActive(false);
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
