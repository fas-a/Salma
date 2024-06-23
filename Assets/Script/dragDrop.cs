using UnityEngine;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    public short tumbuk = 0;
    public short countPotong = 0;
    private string colliderName;
    public panci panci;
    public pisau pisau;
    public ulekan ulekan;
    public parutan parutan;
    public bool isPotong = false;
    public bool isUlek = false;
    public bool isParut = false;
    private bool pindahParut = false;
    public Rigidbody2D rb;
    public Sprite potong;
    public GameObject image;
    private bool inDrag = false;
    public GameObject tas;
    public GameObject customers;
    public Sprite tumbukSprite;

    private void Start()
    {
        cam = Camera.main;
        GameObject pisauObj = GameObject.Find("pisaupotong");
        pisau = pisauObj.GetComponent<pisau>();
        GameObject ulekanObj = GameObject.Find("ulekanpakai");
        ulekan = ulekanObj.GetComponent<ulekan>();
        GameObject parutanObj = GameObject.Find("parutanpakai");
        parutan = parutanObj.GetComponent<parutan>();
        GameObject panciObj = GameObject.FindWithTag("panci");
        panci = panciObj.GetComponent<panci>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(gameObject.tag == "madu" || gameObject.tag == "jeruknipis")
        {
            isPotong = true;
            isUlek = true;
        }
        tas = GameObject.FindWithTag("tas");
        customers = GameObject.FindWithTag("customers");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        colliderName = other.gameObject.name;
        Debug.Log(colliderName + " entered collider");
        if(other.gameObject.tag == "talenan" && isPotong == false)
        {
            cam.transform.position = new Vector3(-50, 0, -10);
            rb.bodyType = RigidbodyType2D.Static;
            pisau.setBahan(gameObject);
            gameObject.transform.position = new Vector3(-49.5f, -4.5f, 0);
            gameObject.transform.localScale = new Vector3(3, 3, 1);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            tas.SetActive(false);
            customers.SetActive(false);
        }
        if(other.gameObject.tag == "ulekan" && isUlek == false)
        {
            cam.transform.position = new Vector3(-100, 0, -10);
            rb.bodyType = RigidbodyType2D.Static;
            ulekan.setBahan(gameObject);
            gameObject.transform.position = new Vector3(-100f, -4.5f, 0);
            gameObject.transform.localScale = new Vector3(3, 3, 1);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            tas.SetActive(false);
            customers.SetActive(false);
        }
        if(other.gameObject.tag == "panci" && inDrag == false)
        {
            Debug.Log("Panci");
            panci.addItem(gameObject.tag);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "parutan" && isParut == false)
        {
            Debug.Log("Parutan");
            pindahParut = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "parutan")
        {
            pindahParut = false;
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
        if(pindahParut)
        {
            cam.transform.position = new Vector3(-150, 0, -10);
            rb.bodyType = RigidbodyType2D.Static;
            parutan.setBahan(gameObject);
            gameObject.transform.position = new Vector3(-149.5f, -4.5f, 0);
            gameObject.transform.localScale = new Vector3(3, 3, 1);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            tas.SetActive(false);
        }
        inDrag = false;
    }
    
    private Vector3 GetMouseWorldPos()
    {
        // Konversi posisi mouse dari screen space ke world space
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    public void potongBahan()
    {
        Destroy(image.GetComponent<PolygonCollider2D>());
        SpriteRenderer spriteRenderer = image.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = potong;
        isPotong = true;
        image.AddComponent<PolygonCollider2D>();
        gameObject.tag = gameObject.tag + " potong";
    }

    public void gepeng()
    {
        Destroy(image.GetComponent<PolygonCollider2D>());
        SpriteRenderer spriteRenderer = image.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tumbukSprite;
        isUlek = true;
        image.AddComponent<PolygonCollider2D>();
        gameObject.tag = gameObject.tag + " geprek";
    }

    public void setAwal()
    {
        gameObject.transform.position = new Vector3(-9, -2, 0);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        rb.bodyType = RigidbodyType2D.Dynamic;
        tas.SetActive(true);
        customers.SetActive(true);
    }
}
