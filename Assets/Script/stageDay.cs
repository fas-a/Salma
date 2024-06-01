using UnityEngine;
using TMPro;

public class stageDay : MonoBehaviour
{
    public int hari;
    public TMP_Text teksHari;
    public int targetPesananHarian; // jumlah pesanan yang diharapkan per hari
    public Progress progressScript; // Referensi ke skrip Progress

    void Start()
    {
        hari = 1;
        UpdateDayText();
        MulaiHariBaru();        
    }

    public void MulaiHariBaru()
    {
        progressScript.SetTargetPesananHarian(targetPesananHarian);
        progressScript.ResetPesananHarian();
    }

    public void HariBerikutnya()
    {
        hari++;
        UpdateDayText();
        MulaiHariBaru();
    }

    void UpdateDayText()
    {
        teksHari.text = hari + "/30";
    }
    
    void Update()
    {
        
    }
}
