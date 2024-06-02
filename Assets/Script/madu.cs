using UnityEngine;
using UnityEngine.SceneManagement;

public class madu : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private string colliderName;
    public panci panci;
    private bool inDrag = false;

    private void Start()
    {
        cam = Camera.main;
        GameObject panciObj = GameObject.FindWithTag("panci");
        panci = panciObj.GetComponent<panci>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        colliderName = other.gameObject.name;
        Debug.Log(colliderName + " entered collider");
        if(other.gameObject.tag == "panci" && inDrag == false)
        {
            Debug.Log("Panci");
            panci.addItem(gameObject.tag);
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        // Hitung offset antara posisi mouse dan posisi objek
        offset = gameObject.transform.position - GetMouseWorldPos();
        inDrag = true;
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
        inDrag = false;
    }
    

    private Vector3 GetMouseWorldPos()
    {
        // Konversi posisi mouse dari screen space ke world space
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

}
