using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class itemWarung : MonoBehaviour
{
    public int harga;
    public string nama;
    public Sprite image;
    public Image targetImage;
    public TMP_Text teksHarga;
    public TMP_Text teksNama;

    private void Start()
    {
        targetImage.sprite = image;
        teksHarga.text = "Rp " + harga.ToString();
        teksNama.text = nama;
    }

}
