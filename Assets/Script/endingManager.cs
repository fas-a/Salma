using UnityEngine;
using TMPro;

public class endingManager : MonoBehaviour, IDataPersistence
{
    public GameObject goodEnding, badEnding, successStamp, failureStamp;
    public Animator succesStampAnimation, failureStampAnimation;
    private int targetMoney = 2000000;
    private GameData gameData;
    public TMP_Text textPenghasilan, textLabaKotor, textPengeluaran, textJumlahJamuSederhana, textJumlahJamuKunyitAsam, textJumlahJamuBerasKencur, textJumlahJamuPahitan, textJumlahJamuTemulawak, textHargaJamuSederhana, textHargaJamuKunyitAsam, textHargaJamuBerasKencur, textHargaJamuPahitan, textHargaJamuTemulawak;
    private int money, expenses, grossProfit, orderCompleted, jumlahJamuSederhana, jumlahJamuKunyitAsam, jumlahJamuBerasKencur, jumlahJamuPahitan, jumlahJamuTemulawak;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showCalculation();
    }

    public void LoadData(GameData data)
    {
        this.jumlahJamuSederhana = data.jumlahJamuSederhana;
        this.jumlahJamuKunyitAsam = data.jumlahJamuKunyitAsam;
        this.jumlahJamuBerasKencur = data.jumlahJamuBerasKencur;
        this.jumlahJamuPahitan = data.jumlahJamuPahitan;
        this.jumlahJamuTemulawak = data.jumlahJamuTemulawak;
        this.money = data.money;
        this.expenses = data.expenses;
        this.grossProfit = data.grossProfit;
    }

    public void SaveData(GameData data)
    {
        data.jumlahJamuSederhana = this.jumlahJamuSederhana;
        data.jumlahJamuKunyitAsam = this.jumlahJamuKunyitAsam;
        data.jumlahJamuBerasKencur = this.jumlahJamuBerasKencur;
        data.jumlahJamuPahitan = this.jumlahJamuPahitan;
        data.jumlahJamuTemulawak = this.jumlahJamuTemulawak;
        data.money = this.money;
        data.expenses = this.expenses;
        data.grossProfit = this.grossProfit;
    }

    public void showCalculation()
    {
        textJumlahJamuSederhana.text = jumlahJamuSederhana + "x";
        textJumlahJamuKunyitAsam.text = jumlahJamuKunyitAsam + "x";
        textJumlahJamuBerasKencur.text = jumlahJamuBerasKencur + "x";
        textJumlahJamuPahitan.text = jumlahJamuPahitan + "x";
        textJumlahJamuTemulawak.text = jumlahJamuTemulawak + "x";

        int hasilJamuSederhana = jumlahJamuSederhana * 10000;
        int hasilJamuKunyitAsam = jumlahJamuKunyitAsam * 11000;
        int hasilJamuBerasKencur = jumlahJamuBerasKencur * 11000;
        int hasilJamuPahitan = jumlahJamuPahitan * 14000;
        int hasilJamuTemulawak = jumlahJamuTemulawak * 18000;

        textHargaJamuSederhana.text = "Rp" + hasilJamuSederhana;
        textHargaJamuKunyitAsam.text = "Rp" + hasilJamuKunyitAsam;
        textHargaJamuBerasKencur.text = "Rp" + hasilJamuBerasKencur;
        textHargaJamuPahitan.text = "Rp" + hasilJamuPahitan;
        textHargaJamuTemulawak.text = "Rp" + hasilJamuTemulawak;
        textPenghasilan.text = "Rp" + money;
        textLabaKotor.text = "Rp" + grossProfit;
        textPengeluaran.text = "Rp" + expenses;

        if (money >= targetMoney)
        {
            successStamp.SetActive(true);
            failureStamp.SetActive(false);
            succesStampAnimation.Play("successStamp");
        }
        else
        {
            failureStamp.SetActive(true);
            successStamp.SetActive(false);
            failureStampAnimation.Play("failureStamp");
        }
    }

    public void showEnding()
    {
        if (money >= targetMoney)
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
