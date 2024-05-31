using UnityEngine;
using TMPro;

public class progress : MonoBehaviour
{
    private int uang;
    private int jumlahPesanan;
    public TMP_Text teksUang;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uang = 0;
        jumlahPesanan = 0;
        itemPesanan.OnOrderCompleted += HandleOrderCompleted;
    }

    void HandleOrderCompleted(int price)
    {
        jumlahPesanan++;
        uang += price;
    }

    // Update is called once per frame
    void Update()
    {
        string order = "Pesanan yang diselesaikan: " + jumlahPesanan;
        string money = "Uang: Rp. " + uang;
        teksUang.text = order + "\n" + money;
    }

    private void OnDestroy()
    {
        itemPesanan.OnOrderCompleted -= HandleOrderCompleted;
    }
}
