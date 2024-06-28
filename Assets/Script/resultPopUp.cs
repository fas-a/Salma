using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class resultPopUp : MonoBehaviour
{
    public TMP_Text teksHari;
    public TMP_Text teksPesanan;
    public stageDay dayScript;
    public GameObject modal;
    private int day;

    public void displayResult(int hari, int pesanan) {
        this.day = hari;
        teksHari.text = "Hari ke-" + hari;
        teksPesanan.text = "Hari selesai!\n Total pesanan: " + pesanan;
        modal.SetActive(true);
    }

    public void HideResult()
    {
        modal.SetActive(false); // Nonaktifkan objek popup
    }

    public void OnContinueButtonClick()
    {
        HideResult();

        if (day < 30)
        {
            dayScript.HariBerikutnya();
        }
        else
        {
            GamePersistenceManager.instance.LoadEndingScene();
        }
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
