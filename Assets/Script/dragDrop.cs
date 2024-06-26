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
    public bool isPotong = false;
    public bool isUlek = false;
    private bool pindahParut = false;
    public Rigidbody2D rb;
    public Sprite potong;
    public GameObject image;
    private bool inDrag = false;
    public GameObject tas;
    public GameObject customers;
    public Sprite tumbukSprite;
    private bool use = false;

    private void Start()
    {
        cam = Camera.main;
        GameObject pisauObj = GameObject.Find("pisaupotong");
        pisau = pisauObj.GetComponent<pisau>();
        GameObject ulekanObj = GameObject.Find("ulekanpakai");
        ulekan = ulekanObj.GetComponent<ulekan>();
        GameObject panciObj = GameObject.FindWithTag("panci");
        panci = panciObj.GetComponent<panci>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        tas = GameObject.FindWithTag("tas");
        customers = GameObject.FindWithTag("customers");
    }

    private void pindahAlat(int x, float y)
    {
        use = true;
        cam.transform.position = new Vector3(x, 0, -10);
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.transform.position = new Vector3(x, y, 0);
        gameObject.transform.localScale = new Vector3(3, 3, 1);
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        tas.SetActive(false);
        customers.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        colliderName = other.gameObject.name;
        Debug.Log(colliderName + " entered collider");
        if(!inDrag)
        {
            if(other.gameObject.tag == "talenan")
            {
                alatDapur alatDapur = other.collider.gameObject.GetComponent<alatDapur>();
                if(!isPotong && alatDapur.currentDurability > 0)
                {
                    alatDapur.kurang();
                    pisau.setBahan(gameObject);
                    pindahAlat(-50, -4.5f);
                }
                else
                {
                    gameObject.transform.position = new Vector3(-10, -3, 0);
                }
            }
            if(other.gameObject.tag == "ulekan")
            {
                alatDapur alatDapur = other.collider.gameObject.GetComponent<alatDapur>();
                if(!isUlek && alatDapur.currentDurability > 0)
                {
                    alatDapur.kurang();
                    ulekan.setBahan(gameObject);
                    pindahAlat(-100, -4.5f);
                }
                else
                {
                    gameObject.transform.position = new Vector3(-10, -3, 0);
                }
            }
            if(other.gameObject.tag == "panci")
            {
                alatDapur alatDapur = other.collider.gameObject.GetComponent<alatDapur>();
                if (alatDapur.currentDurability > 0)
                {
                    Debug.Log("Panci");
                    panci.addItem(gameObject.tag);
                    Destroy(gameObject);
                }
                else
                {
                    gameObject.transform.position = new Vector3(-10, -3, 0);
                }
            }
        }
    }


    private void OnMouseDown()
    {
        if(!use)
        {
            offset = gameObject.transform.position - GetMouseWorldPos();
            inDrag = true;
        }
    }

    private void OnMouseDrag()
    {
        if(!use)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        if(!use)
        {
            inDrag = false;
        }
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
        isUlek = true;
        image.AddComponent<PolygonCollider2D>();
        gameObject.tag = gameObject.tag + " Potong";
    }

    public void gepeng()
    {
        Destroy(image.GetComponent<PolygonCollider2D>());
        SpriteRenderer spriteRenderer = image.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tumbukSprite;
        isUlek = true;
        isPotong = true;
        image.AddComponent<PolygonCollider2D>();
        if(gameObject.tag == "Jahe")
        {
            gameObject.tag = gameObject.tag + " Geprek";
        }
        else
        {
            gameObject.tag = gameObject.tag + " Halus";
        }
    }

    public void setAwal()
    {
        gameObject.transform.position = new Vector3(-10, -3, 0);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        rb.bodyType = RigidbodyType2D.Dynamic;
        tas.SetActive(true);
        customers.SetActive(true);
        use = false;
    }
}
