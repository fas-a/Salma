using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;


    private void Start()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        // Hitung offset antara posisi mouse dan posisi objek
        offset = gameObject.transform.position - GetMouseWorldPos();
        Debug.Log("down");
    }

    private void OnMouseDrag()
    {
        // Update posisi objek mengikuti posisi mouse
        transform.position = GetMouseWorldPos() + offset;
        Debug.Log("drag");
    }

    private void OnMouseUp()
    {
        Debug.Log("up");
        // RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        // if (hit.collider != null)
        // {
        //     Debug.Log(hit.collider.name);
        //     // Jika objek yang terkena adalah slotItem, panggil metode OnMouseUp dari slotItem
        //     slotItem slot = hit.collider.GetComponent<slotItem>();
        //     if (slot != null)
        //     {
        //         slot.OnMouseUp();
        //         Destroy(gameObject);
        //     }
        // }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Konversi posisi mouse dari screen space ke world space
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }
}
