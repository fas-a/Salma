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
    public Sprite jamuSederhana;
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
        jamuResep.Add(new Resep("Jamu Sederhana", new List<string> {"air", "kunyit potong", "jahe potong", "sereh potong", "madu", "jeruknipis"}, jamuSederhana));
        // jamuResep.Add(new Resep("Jamu Sederhana", new List<string> {"air"}, jamuSederhana));
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
            addItem("air");
            image.sprite = isi;
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
                    gelas.image.sprite = gelas.jamuSederhana;
                    gelas.jamu = true;
                    gelas.gelasJamu = "jamuSederhana";
                    break;
            }
            image.sprite = kosong;
            jamu = false;
            namaJamu = "";
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
            }
        }
    }

    private bool ContainsAllItems(List<string> resepItems)
    {
        foreach (string item in resepItems)
        {
            if (!items.Contains(item))
            {
                return false;
            }
        }
        return true;
    }
}
