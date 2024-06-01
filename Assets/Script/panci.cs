using UnityEngine;
using UnityEngine.UI;

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
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        Debug.Log("up");
        if(isGalon){
            Debug.Log("Panci terisi");
            image.sprite = isi;
        } else {
            Debug.Log("Panci tidak terisi");
            image.sprite = kosong;
        }
        balik();
    }
    

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

}
