using UnityEngine;

public class ulekan : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        // if (collision.gameObject.CompareTag("Enemy")) 
        // { 
        //     collision.gameObject.SendMessage("ApplyDamage", 10); 
        // } 
        Debug.Log("Collision with " + collision.gameObject.name);
        DragAndDrop drag = collision.gameObject.GetComponent<DragAndDrop>();
        if (drag != null)
        {
            drag.pukul();
        }
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        // Hitung offset antara posisi mouse dan posisi objek
        offset = gameObject.transform.position - GetMouseWorldPos();
        Debug.Log("down");
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    

    private Vector3 GetMouseWorldPos()
    {
        // Konversi posisi mouse dari screen space ke world space
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }
}
