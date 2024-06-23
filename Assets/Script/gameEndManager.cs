using UnityEngine;
using TMPro;

public class gameEndManager : MonoBehaviour
{
    public GameObject goodEnding;
    public GameObject badEnding;
    private int targetMoney = 2000000;
    private GameData gameData;
    public TMP_Text textJumlahJamuSederhana;
    public TMP_Text textJumlahJamuKunyitAsam;
    public TMP_Text textJumlahJamuBerasKencur;
    public TMP_Text textJumlahJamuPahitan;
    public TMP_Text textJumlahJamuTemulawak;
    public TMP_Text textHargaJamuSederhana;
    public TMP_Text textHargaJamuKunyitAsam;
    public TMP_Text textHargaJamuBerasKencur;
    public TMP_Text textHargaJamuPahitan;
    public TMP_Text textHargaJamuTemulawak;
    public TMP_Text textPenghasilan;
    public TMP_Text textLabaKotor;
    public TMP_Text textPengeluaran;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameData = GamePersistenceManager.instance.GetGameData();
        showCalculation();
    }

    public void showCalculation()
    {
        textJumlahJamuSederhana.text = gameData.jumlahJamuSederhana + "x";
        textJumlahJamuKunyitAsam.text = gameData.jumlahJamuKunyitAsam + "x";
        textJumlahJamuBerasKencur.text = gameData.jumlahJamuBerasKencur + "x";
        textJumlahJamuPahitan.text = gameData.jumlahJamuPahitan + "x";
        textJumlahJamuTemulawak.text = gameData.jumlahJamuTemulawak + "x";
        
        int hasilJamuSederhana = gameData.jumlahJamuSederhana * 12000;
        int hasilJamuKunyitAsam = gameData.jumlahJamuKunyitAsam * 17000;
        int hasilJamuBerasKencur = gameData.jumlahJamuBerasKencur * 20000;
        int hasilJamuPahitan = gameData.jumlahJamuPahitan * 22000;
        int hasilJamuTemulawak = gameData.jumlahJamuTemulawak * 22000;

        textHargaJamuSederhana.text = "Rp" + hasilJamuSederhana;
        textHargaJamuKunyitAsam.text = "Rp" + hasilJamuKunyitAsam;
        textHargaJamuBerasKencur.text = "Rp" + hasilJamuBerasKencur;
        textHargaJamuPahitan.text = "Rp" + hasilJamuPahitan;
        textHargaJamuTemulawak.text = "Rp" + hasilJamuTemulawak;

        textPenghasilan.text = "Rp" + gameData.money;
        textLabaKotor.text = "Rp" + gameData.grossProfit;
        textPengeluaran.text = "Rp" + gameData.expenses;
    }

    public void showEnding()
    {
        if (gameData.money >= targetMoney)
        {
            ShowHappyEnding();
        }
        else
        {
            ShowBadEnding();
        }
    }

    public void ShowHappyEnding()
    {
        goodEnding.SetActive(true);
    }

    public void ShowBadEnding()
    {
        badEnding.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
