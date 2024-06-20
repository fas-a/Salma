using UnityEngine;
using TMPro;

public class Progress : MonoBehaviour, IDataPersistence
{
    private int uang = 10000;
    private int jumlahPesanan;
    public TMP_Text teksUang;
    public TMP_Text teksTimer;
    public stageDay dayCounterScript;

    void Start()
    {
        ItemPesanan.OnOrderCompleted += HandleOrderCompleted;
    }

    public int getUang()
    {
        return this.uang;
    }

    public void beli(int price)
    {
        uang -= price;
        Update();
    }

    void HandleOrderCompleted(int price)
    {
        jumlahPesanan++;
        uang += price;
    }

    public int GetJumlahPesanan() {
        return jumlahPesanan;
    }

    public void LoadData(GameData data) {
        this.jumlahPesanan = data.orderCompleted;
        this.uang = data.money;
    }

    public void SaveData(GameData data) {
        data.orderCompleted = this.jumlahPesanan;
        data.money = this.uang;
    }

    public void UpdateTimer(float waktuTersisa, float durasiHari, int jamMulai)
    {
        float conversionFactor = 15f / durasiHari; // Karena 15 jam dalam permainan setara dengan durasi hari dalam waktu nyata

        // Menghitung total jam berdasarkan faktor konversi
        float totalJam = (durasiHari - waktuTersisa) * conversionFactor;

        int jam = jamMulai + (int)totalJam;
        int menit = (int)((totalJam - (int)totalJam) * 60f); // Mengkonversi sisa menit dari desimal ke integer

        string waktu = string.Format("{0:00}:{1:00}", jam, menit);
        teksTimer.text = "Waktu: " + waktu;
    }

    void Update()
    {
        string order = "Pesanan yang diselesaikan: " + jumlahPesanan;
        string money = "Uang: Rp. " + uang;
        teksUang.text = order + "\n" + money; 
    }

    void OnDestroy()
    {
        ItemPesanan.OnOrderCompleted -= HandleOrderCompleted;
    }
}
