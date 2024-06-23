using UnityEngine;

public class sendok : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private Vector3 posisiAwal;
    private bool isPanci = false;
    public Rigidbody2D rb;
    public panci panci;
    void Start()
    {
        cam = Camera.main;
        posisiAwal = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if(collision.gameObject.name == "panci"){
            isPanci = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) 
    { 
        if(collision.gameObject.name == "panci"){
            isPanci = false;
        }
    }

    private void balik()
    {
        transform.position = posisiAwal;
    }

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        if(isPanci)
        {
            Debug.Log("Sendok dimasukkan ke panci");
            panci.CheckResep();
        }
        balik();
        rb.bodyType = RigidbodyType2D.Static;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }
}
