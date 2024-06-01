using UnityEngine;
using TMPro;

public class Progress : MonoBehaviour
{
    private int uang;
    private int jumlahPesanan;
    private int pesananHarian;
    public TMP_Text teksUang;
    public stageDay dayCounterScript; // Pastikan ini sesuai dengan referensi ke skrip DayCounter

    void Start()
    {
        uang = 0;
        jumlahPesanan = 0;
        pesananHarian = 0;
        itemPesanan.OnOrderCompleted += HandleOrderCompleted;
    }

    public void SetTargetPesananHarian(int target)
    {
        pesananHarian = target;
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

        // Periksa apakah target pesanan harian tercapai
        if (pesananHarian >= dayCounterScript.targetPesananHarian)
        {
            dayCounterScript.HariBerikutnya();
        }
    }

    void Update()
    {
        string order = "Pesanan yang diselesaikan: " + jumlahPesanan;
        string money = "Uang: Rp. " + uang;
        teksUang.text = order + "\n" + money;
    }

    void OnDestroy()
    {
        itemPesanan.OnOrderCompleted -= HandleOrderCompleted;
    }
}
