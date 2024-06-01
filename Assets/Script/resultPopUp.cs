using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class resultPopUp : MonoBehaviour
{
    public TMP_Text teksHari;
    public TMP_Text teksPesanan;
    public stageDay dayScript;
    public GameObject modal;

    public void displayResult(string hari, string pesanan) {
        teksHari.text = hari;
        teksPesanan.text = pesanan;
        modal.SetActive(true);
    }

    public void HideResult()
    {
        modal.SetActive(false); // Nonaktifkan objek popup
    }

    public void OnContinueButtonClick()
    {
        HideResult();

        dayScript.HariBerikutnya();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
