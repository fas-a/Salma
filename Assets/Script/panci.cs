using UnityEngine;
using System.Collections.Generic;

public class panci : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 posisiAwal;
    private Camera cam;
    private bool isGalon = false;
    public galon galon;
    public SpriteRenderer image;
    public Sprite kosong;
    public Sprite isi;
    public Rigidbody2D rb;
    public List<string> items;
    private List<Resep> jamuResep;
    public Sprite jamuSederhana;
    public bool jamu = false;
    public string namaJamu;

    private void Start()
    {
        cam = Camera.main;
        posisiAwal = transform.position;
        items = new List<string>();
        InitializeJamuResep();
    }

    private void InitializeJamuResep()
    {
        jamuResep = new List<Resep>();
        jamuResep.Add(new Resep("Jamu Sederhana", new List<string> {"air", "kunyitpotong", "jahepotong", "serehpotong", "madu", "jeruknipis"}, jamuSederhana));
    }

    private void balik()
    {
        transform.position = posisiAwal;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if(collision.gameObject.name == galon.name){
            isGalon = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) 
    { 
        if(collision.gameObject.name == galon.name){
            isGalon = false;
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
        if(isGalon){
            Debug.Log("Panci terisi");
            addItem("air");
            image.sprite = isi;
        } else {
            Debug.Log("Panci tidak terisi");
            image.sprite = kosong;
            items.Remove("air");
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

    public void addItem(string item)
    {
        items.Add(item);
        CheckResep();
    }

    private void CheckResep()
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
