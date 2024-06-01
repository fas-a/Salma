using UnityEngine;
using TMPro;

public class stageDay : MonoBehaviour
{
    public int hari;
    public TMP_Text teksHari;
    public Progress progressScript;
    public Pesanan orderSpawner;
    public float durasiHari = 10800f; // Durasi satu hari dalam detik
    private float waktuTersisa;
    private int jamMulai = 7; // Jam mulai pukul 7 pagi

    void Start()
    {
        hari = 1;
        UpdateDayText();
        MulaiHariBaru();
    }

    void Update()
    {
        HitungMundurWaktu();
    }

    void HitungMundurWaktu()
    {
        if (waktuTersisa > 0)
        {
            waktuTersisa -= Time.deltaTime;
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
            HariBerikutnya();
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
