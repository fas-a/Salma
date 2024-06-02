using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class itemWarung : MonoBehaviour
{
    public Progress proses;
    public int harga;
    public string nama;
    public Sprite image;
    public Image targetImage;
    public TMP_Text teksHarga;
    public TMP_Text teksNama;
    public slotItem gudang;
    public TMP_Text tabungan;

    private void Start()
    {
        targetImage.sprite = image;
        teksHarga.text = "Rp " + harga.ToString();
        teksNama.text = nama;
        tabungan.text = "Rp " + proses.getUang().ToString();
    }

    public void beli()
    {
        if(proses.getUang() >= harga){
            proses.beli(harga);
            gudang.add();
            tabungan.text = "Rp " + proses.getUang().ToString();
        }
    }

}
