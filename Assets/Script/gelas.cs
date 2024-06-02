using UnityEngine;

public class gelas : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 posisiAwal;
    private Camera cam;
    public SpriteRenderer image;
    public Sprite kosong;
    public Rigidbody2D rb;
    public Sprite jamuSederhana;
    private bool isPanci = false;
    private bool isSerahkan = false;
    public panci panci;
    private bool jamu = false;
    public GameObject serahkan;
    public Pesanan boxPesanan;
    private string gelasJamu;

    private void Start()
    {
        cam = Camera.main;
        posisiAwal = transform.position;
    }

    private void balik()
    {
        transform.position = posisiAwal;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if(collision.gameObject.name == panci.name){
            isPanci = true;
        }
        if(collision.gameObject.name == serahkan.name){
            isSerahkan = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) 
    { 
        if(collision.gameObject.name == panci.name){
            isPanci = false;
        }
        if(collision.gameObject.name == serahkan.name){
            isSerahkan = false;
        }
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
        Debug.Log(this.isPanci);
        Debug.Log("up");
        if(isPanci && panci.jamu){
            switch(panci.namaJamu){
                case "Jamu Sederhana":
                    image.sprite = jamuSederhana;
                    jamu = true;
                    panci.image.sprite = panci.kosong;
                    panci.jamu = false;
                    gelasJamu = "jamuSederhana";
                    break;
            }
        }
        if(isSerahkan && jamu){
            Debug.Log(gelasJamu);
            ItemPesanan matchingOrder = boxPesanan.GetMatchingOrder(gelasJamu);
            Debug.Log(matchingOrder);
            if(matchingOrder != null)
            {
                matchingOrder.CompleteOrder(true);
                boxPesanan.RemoveOrder(matchingOrder);
                image.sprite = kosong;
                jamu = false;
            }
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
