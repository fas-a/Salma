using UnityEngine;
using TMPro;

public class parutan : MonoBehaviour
{
    private short tumbuk = 0;
    private Vector3 posisiAwal;
    private Camera cam;
    private GameObject bahan;
    private DragAndDrop dragDrop;
    public TMP_Text teks;
    public GameObject back;
    
    void Start()
    {
        cam = Camera.main;
        posisiAwal = transform.position;
    }

    public void setBahan(GameObject bahan)
    {
        this.bahan = bahan;
        dragDrop = bahan.GetComponent<DragAndDrop>();
        tumbuk = dragDrop.tumbuk;
        back.SetActive(true);
    }

    public void reset()
    {
        back.SetActive(false);
        cam.transform.position = new Vector3(0, 0, -10);
        dragDrop.setAwal();
    }
}
