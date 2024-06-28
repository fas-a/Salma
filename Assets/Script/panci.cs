using UnityEngine;
using System.Collections.Generic;

public class panci : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 posisiAwal;
    private Camera cam;
    private bool isGalon = false;
    private bool isBuang = false;
    private bool isDrag = false;
    private bool isGelas = false;
    public SpriteRenderer image;
    public Sprite kosong;
    public Sprite isi;
    public Rigidbody2D rb;
    public List<string> items;
    private List<Resep> jamuResep;
    private alatDapur alatDapur;
    public Sprite kuning;
    public Sprite hitam;
    public Sprite gagal;
    public bool jamu = false;
    public string namaJamu;
    public bahanPanci tooltip;

    private void Start()
    {
        cam = Camera.main;
        posisiAwal = transform.position;
        items = new List<string>();
        alatDapur = gameObject.GetComponent<alatDapur>();
        InitializeJamuResep();
    }

    private void InitializeJamuResep()
    {
        jamuResep = new List<Resep>();
        jamuResep.Add(new Resep("Jamu Sederhana", new List<string> {"Air", "Kunyit Potong", "Jahe Potong", "Sereh Potong", "Madu", "Jeruk Nipis"}, kuning));
        jamuResep.Add(new Resep("Jamu Kunyit Asam", new List<string> {"Air", "Kunyit Halus", "Asam Jawa", "Gula Merah Potong"}, kuning));
        jamuResep.Add(new Resep("Jamu Beras Kencur", new List<string> {"Air", "Jahe Geprek", "Gula Merah Potong", "Gula Pasir", "Kencur Halus", "Beras Halus"}, kuning));
        jamuResep.Add(new Resep("Jamu Temulawak", new List<string> {"Air", "Temu Lawak", "Temu Lawak", "Madu"}, kuning));
        jamuResep.Add(new Resep("Jamu Pahitan", new List<string> {"Air", "Kunyit Halus", "Temu Lawak Halus", "Temu Hitam Halus", "Brotowali Halus", "Daun Sirih", "Gula Merah Potong", "Asam Jawa"}, hitam));
        // jamuResep.Add(new Resep("Jamu Sederhana", new List<string> {"Air"}, jamuSederhana));
    }

    private void balik()
    {
        transform.position = posisiAwal;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if(collision.gameObject.name == "galon"){
            isGalon = true;
        }
        if(collision.gameObject.name == "tempatsampah"){
            isBuang = true;
        }
        if(collision.gameObject.name == "gelas"){
            isGelas = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) 
    { 
        if(collision.gameObject.name == "galon"){
            isGalon = false;
        }
        if(collision.gameObject.name == "tempatsampah"){
            isBuang = false;
        }
        if(collision.gameObject.name == "gelas"){
            isGelas = false;
        }
    }

    private void OnMouseEnter()
    {
        if(!isDrag)
        {
            tooltip.showBahan();
        }
    }

    private void OnMouseExit()
    {
        tooltip.hideBahan();
    }

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
        rb.bodyType = RigidbodyType2D.Dynamic;
        tooltip.hideBahan();
        isDrag = true;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        if(isGalon){
            Debug.Log("Panci terisi");
            GameObject galonObj = GameObject.Find("galon");
            galon galon = galonObj.GetComponent<galon>();
            if(galon.currentDurability > 0)
            {
                galon.kurang();
                addItem("Air");
                image.sprite = isi;
            }
        }
        if(isBuang){
            Debug.Log("Panci terbuang");
            items.Clear();
            image.sprite = kosong;
            jamu = false;
            namaJamu = "";
            tooltip.clearBahan();
            alatDapur.kurang();
        }
        if(isGelas && jamu)
        {
            gelas gelas = GameObject.Find("gelas").GetComponent<gelas>();
            switch(namaJamu)
            {
                case "Jamu Sederhana":
                    Debug.Log("Jamu Sederhana");
                    gelas.image.sprite = gelas.jamus[0];
                    gelas.gelasJamu = "jamuSederhana";
                    break;
                case "Jamu Kunyit Asam":
                    gelas.image.sprite = gelas.jamus[1];
                    gelas.gelasJamu = "jamuKunyitAsam";
                    break;
                case "Jamu Beras Kencur":
                    gelas.image.sprite = gelas.jamus[2];
                    gelas.gelasJamu = "jamuBerasKencur";
                    break;
                case "Jamu Temulawak":
                    gelas.image.sprite = gelas.jamus[3];
                    gelas.gelasJamu = "jamuTemulawak";
                    break;
                case "Jamu Pahitan":
                    gelas.image.sprite = gelas.jamus[4];
                    gelas.gelasJamu = "jamuPahitan";
                    break;
            }
            gelas.jamu = true;
            image.sprite = kosong;
            jamu = false;
            namaJamu = "";
            tooltip.clearBahan();
            alatDapur.kurang();
        }
        isDrag = false;

        balik();
        rb.bodyType = RigidbodyType2D.Static;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    public void addItem(string item)
    {
        tooltip.addBahan(item);
        items.Add(item);
    }

    public void CheckResep()
    {
        foreach (var resep in jamuResep)
        {
            if (ContainsAllItems(resep.items))
            {
                Debug.Log($"Resep {resep.name} terbentuk!");
                image.sprite = resep.sprite;
                jamu = true;
                namaJamu = resep.name;
                tooltip.clearBahan();
                tooltip.addBahan(resep.name);
                return;
            }
        }
    }

    private bool ContainsAllItems(List<string> resepItems)
    {
        List<string> itemsCopy = new List<string>(items);

        foreach (string item in resepItems)
        {
            if (!itemsCopy.Contains(item))
            {
                return false;
            }
            else
            {
                itemsCopy.Remove(item);
            }
        }
        return true;
    }
}
