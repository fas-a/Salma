using UnityEngine;
using TMPro;

public class Progress : MonoBehaviour
{
    private int uang;
    private int jumlahPesanan;
    private int pesananHarian;
    public TMP_Text teksUang;
    public TMP_Text teksTimer;
    public stageDay dayCounterScript;

    public float conversionFactor = 1f; // 1 detik waktu nyata = 1 menit waktu permainan

    void Start()
    {
        if (teksUang == null)
        {
            Debug.LogError("teksUang is not assigned!");
        }

        if (teksTimer == null)
        {
            Debug.LogError("teksTimer is not assigned!");
        }

        if (dayCounterScript == null)
        {
            Debug.LogError("dayCounterScript is not assigned!");
        }

        jumlahPesanan = 0;
        pesananHarian = 0;
        itemPesanan.OnOrderCompleted += HandleOrderCompleted;
    }

    public void ResetPesananHarian()
    {
        pesananHarian = 0;
    }

    void HandleOrderCompleted(int price)
    {
        jumlahPesanan++;
        pesananHarian++;
        uang += price;
    }

    public void UpdateTimer(float waktuTersisa, float durasiHari, int jamMulai)
    {
        // Menghitung total menit berdasarkan faktor konversi
        float totalMenit = (durasiHari - waktuTersisa) * conversionFactor; // 1 detik = 1 menit waktu permainan
        int jam = jamMulai + (int)(totalMenit / 60);
        int menit = (int)(totalMenit % 60);

        if (jam >= 24)
        {
            jam -= 24;
        }

        string waktu = string.Format("{0:00}:{1:00}", jam, menit);
        teksTimer.text = "Waktu: " + waktu;

        if ((jamMulai + (totalMenit / 60)) >= 22)
        {
            dayCounterScript.HariBerikutnya(); // Ganti ke hari berikutnya
        }
    }


    void Update()
    {
        string order = "Pesanan yang diselesaikan: " + jumlahPesanan;
        string money = "Uang: Rp. " + uang;
        teksUang.text = order + "\n" + money; // Combine order and money text
    }

    void OnDestroy()
    {
        itemPesanan.OnOrderCompleted -= HandleOrderCompleted;
    }
}
