using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class itemWarung : MonoBehaviour
{
    public int harga;
    public string nama;
    public TMP_Text teksHarga;
    public TMP_Text teksNama;

    private void Start()
    {
        teksHarga.text = "Rp " + harga.ToString();
        teksNama.text = nama;
    }

}
