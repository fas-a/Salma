using UnityEngine;
using TMPro;

public class stageDay : MonoBehaviour
{
    public int hari;
    public TMP_Text teksHari;
    public Progress progressScript;
    public Pesanan orderSpawner;
    public float durasiHari = 450f;
    private float waktuTersisa;
    private int jamMulai = 7; // Jam mulai pukul 7 pagi
    public resultPopUp popup;

    void Start()
    {
        hari = 1;
        UpdateDayText();
        MulaiHariBaru();

        InvokeRepeating("HitungMundurWaktu", 0f, 1f);
    }

    void HitungMundurWaktu()
    {
        if (waktuTersisa > 0)
        {
            waktuTersisa -= 1f;
            if (progressScript != null)
            {
                progressScript.UpdateTimer(waktuTersisa, durasiHari, jamMulai); // Update timer in Progress script
            }
            else
            {
                Debug.LogError("progressScript is not assigned in HitungMundurWaktu!");
            }
        }
        else
        {
            HariSelesai();
        }
    }

    

    public void MulaiHariBaru()
    {
        waktuTersisa = durasiHari;
        if (progressScript != null)
        {
            progressScript.ResetPesananHarian();
        }
        else
        {
            Debug.LogError("progressScript is not assigned in MulaiHariBaru!");
        }

        if (orderSpawner != null)
        {
            orderSpawner.SpawnOrders();
        }
        else
        {
            Debug.LogError("orderSpawner is not assigned in MulaiHariBaru!");
        }
    }

    public void HariBerikutnya()
    {
        hari++;
        UpdateDayText();
        MulaiHariBaru();
    }

    public void HariSelesai()
    {
        // Prepare result text, for example:
        string today = "Hari ke-" + hari;
        string result = "Hari selesai!\n Total pesanan: " + progressScript.GetJumlahPesanan();

        // Display the result popup
        popup.displayResult(today, result);
    }

    void UpdateDayText()
    {
        if (teksHari != null)
        {
            teksHari.text = hari + "/30";
        }
        else
        {
            Debug.LogError("teksHari is not assigned in UpdateDayText!");
        }
    }
}
