using UnityEngine;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private short pukulan = 0;
    public Sprite newSprite;
    private string colliderName;

    private void Start()
    {
        cam = Camera.main;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     colliderName = other.gameObject.name;
    //     Debug.Log(colliderName + " entered collider");
    //     if(other.gameObject.tag == "alat")
    //     {
    //         SceneManager.LoadScene(colliderName);
    //     }
    // }

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
        
    }
    

    private Vector3 GetMouseWorldPos()
    {
        // Konversi posisi mouse dari screen space ke world space
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    public void pukul()
    {
        pukulan++;
        Debug.Log("Pukul " + pukulan);
        if (pukulan == 5)
        {
            gepeng();
        }
    }

    private void gepeng()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;
    }
}
