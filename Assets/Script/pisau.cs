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
    public GameObject back;
    private bool animate = false;

    private void Start()
    {
        cam = Camera.main;
        posisiAwal = transform.position;
    }

    public void setBahan(GameObject bahan)
    {
        this.bahan = bahan;
        dragDrop = bahan.GetComponent<DragAndDrop>();
        potong = dragDrop.countPotong;
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
        if(!animate)
        {
            animate = true;
            Debug.Log("down");
            potong++;
            dragDrop.countPotong++;
            StartCoroutine(AnimateMovement());
            tekspotong.text = "Klik Pisau " + (5-dragDrop.countPotong) + " kali lagi";
            if(dragDrop.countPotong == 5)
            {
                tekspotong.text = "Bahan Terpotong";
            }
        }
    }

    private IEnumerator AnimateMovement()
    {
        float duration = 0.5f; // Duration of each movement
        Vector3 originalPosition = transform.position;

        // Move down
        Vector3 targetPosition = originalPosition + Vector3.down * 6;
        yield return MoveToPosition(targetPosition, duration);
        if (dragDrop.countPotong == 5)
        {
            dragDrop.potongBahan();
        }

        // Move up
        targetPosition = originalPosition;
        yield return MoveToPosition(targetPosition, duration);

        if (dragDrop.countPotong == 5)
        {
            targetPosition = posisiAwal;
            yield return MoveToPosition(targetPosition, duration);
            cam.transform.position = new Vector3(0, 0, -10);
            dragDrop.setAwal();
            potong = 0;
            back.SetActive(false);
        }
        animate = false;
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
