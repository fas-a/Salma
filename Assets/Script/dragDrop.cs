using UnityEngine;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private short pukulan = 0;
    public short countPotong = 0;
    public Sprite newSprite;
    private string colliderName;
    public panci panci;
    public pisau pisau;
    private bool isPotong = false;
    private bool isUlek = false;
    public Rigidbody2D rb;
    public Sprite potong;
    public GameObject image;
    private bool inDrag = false;
    public GameObject tas;
    private bool cooldown = false;

    private void Start()
    {
        cam = Camera.main;
        GameObject pisauObj = GameObject.Find("pisaupotong");
        pisau = pisauObj.GetComponent<pisau>();
        GameObject panciObj = GameObject.FindWithTag("panci");
        panci = panciObj.GetComponent<panci>();
        if(gameObject.tag == "madu" || gameObject.tag == "jeruknipis")
        {
            isPotong = true;
            isUlek = true;
        }
        tas = GameObject.FindWithTag("tas");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        colliderName = other.gameObject.name;
        Debug.Log(colliderName + " entered collider");
        if(!cooldown){
            if(other.gameObject.tag == "talenan" && isPotong == false)
            {
                cam.transform.position = new Vector3(-50, 0, -10);
                rb.bodyType = RigidbodyType2D.Static;
                pisau.setBahan(gameObject);
                gameObject.transform.position = new Vector3(-49.5f, -4.5f, 0);
                gameObject.transform.localScale = new Vector3(3, 3, 1);
                tas.SetActive(false);
            }
            if(other.gameObject.tag == "ulekan")
            {
                cam.transform.position = new Vector3(-100, 0, -10);
            }
            if(other.gameObject.tag == "panci" && inDrag == false)
            {
                Debug.Log("Panci");
                panci.addItem(gameObject.tag);
                Destroy(gameObject);
            }
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
        cooldown = false;
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

    public void potongBahan()
    {
        Destroy(image.GetComponent<PolygonCollider2D>());
        SpriteRenderer spriteRenderer = GameObject.Find("image").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = potong;
        gameObject.transform.position = new Vector3(-50f, -2f, 0);
        isPotong = true;
        image.AddComponent<PolygonCollider2D>();
        gameObject.tag = gameObject.tag + "potong";
    }

    private void gepeng()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;
    }

    public void setAwal()
    {
        cooldown = true;
        gameObject.transform.position = new Vector3(-6, -3, 0);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        rb.bodyType = RigidbodyType2D.Dynamic;
        tas.SetActive(true);
    }
}
