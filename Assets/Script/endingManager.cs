using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;

public class endingManager : MonoBehaviour, IDataPersistence
{
    public GameObject goodEnding, badEnding, successStamp, failureStamp;
    public Animator successStampAnimation, failureStampAnimation;
    public PlayableDirector goodEndingTimeline, badEndingTimeline;
    private int targetMoney = 2000000;
    private GameData gameData;
    public TMP_Text textPenghasilan, textLabaKotor, textPengeluaran, textJumlahJamuSederhana, textJumlahJamuKunyitAsam, textJumlahJamuBerasKencur, textJumlahJamuPahitan, textJumlahJamuTemulawak, textHargaJamuSederhana, textHargaJamuKunyitAsam, textHargaJamuBerasKencur, textHargaJamuPahitan, textHargaJamuTemulawak;
    private int money, profit, expenses, grossProfit, orderCompleted, jumlahJamuSederhana, jumlahJamuKunyitAsam, jumlahJamuBerasKencur, jumlahJamuPahitan, jumlahJamuTemulawak;

    void Start()
    {
        Debug.Log("Start method called");
        showCalculation();
    }

    public void LoadData(GameData data)
    {
        Debug.Log("LoadData method called");
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
    }

    public void showCalculation()
    {
        Debug.Log("showCalculation method called");
        textJumlahJamuSederhana.text = jumlahJamuSederhana + "x";
        textJumlahJamuKunyitAsam.text = jumlahJamuKunyitAsam + "x";
        textJumlahJamuBerasKencur.text = jumlahJamuBerasKencur + "x";
        textJumlahJamuPahitan.text = jumlahJamuPahitan + "x";
        textJumlahJamuTemulawak.text = jumlahJamuTemulawak + "x";

        int hasilJamuSederhana = jumlahJamuSederhana * 13000;
        int hasilJamuKunyitAsam = jumlahJamuKunyitAsam * 9500;
        int hasilJamuBerasKencur = jumlahJamuBerasKencur * 16000;
        int hasilJamuPahitan = jumlahJamuPahitan * 15000;
        int hasilJamuTemulawak = jumlahJamuTemulawak * 9000;
        profit = grossProfit - expenses;

        textHargaJamuSederhana.text = "Rp" + hasilJamuSederhana;
        textHargaJamuKunyitAsam.text = "Rp" + hasilJamuKunyitAsam;
        textHargaJamuBerasKencur.text = "Rp" + hasilJamuBerasKencur;
        textHargaJamuPahitan.text = "Rp" + hasilJamuPahitan;
        textHargaJamuTemulawak.text = "Rp" + hasilJamuTemulawak;
        textPenghasilan.text = "Rp" + profit;
        textLabaKotor.text = "Rp" + grossProfit;
        textPengeluaran.text = "Rp" + expenses;
        
        if (money >= targetMoney)
        {
            Debug.Log("Displaying success stamp");
            successStamp.SetActive(true);
            failureStamp.SetActive(false);
            Debug.Log("Before starting PlaySuccessStampAnimation coroutine");
            StartCoroutine(PlaySuccessStampAnimation());
        }
        else
        {
            Debug.Log("Displaying failure stamp");
            failureStamp.SetActive(true);
            successStamp.SetActive(false);
            Debug.Log("Before starting PlayFailureStampAnimation coroutine");
            StartCoroutine(PlayFailureStampAnimation());
        }

    }

    IEnumerator PlaySuccessStampAnimation()
    {
        yield return new WaitForSeconds(0.1f);  // Menambahkan sedikit delay
        Debug.Log("Playing success stamp animation");
        if (successStampAnimation != null)
        {
            Debug.Log("Success stamp animation found, playing animation");
            successStampAnimation.Play("successStamp");
        }
        else
        {
            Debug.LogError("Success stamp animation is null");
        }
    }

    IEnumerator PlayFailureStampAnimation()
    {
        yield return new WaitForSeconds(0.1f);  // Menambahkan sedikit delay
        Debug.Log("Playing failure stamp animation");
        if (failureStampAnimation != null)
        {
            Debug.Log("Failure stamp animation found, playing animation");
            failureStampAnimation.Play("failureStamp");
        }
        else
        {
            Debug.LogError("Failure stamp animation is null");
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
        Debug.Log("Showing Happy Ending");
        goodEnding.SetActive(true);
        if (goodEndingTimeline != null)
        {
            StartCoroutine(PlayGoodEndingTimeline());
        }
        else
        {
            Debug.LogError("Good ending timeline is null");
        }
    }

    public void ShowBadEnding()
    {
        Debug.Log("Showing Bad Ending");
        badEnding.SetActive(true);
        if (badEndingTimeline != null)
        {
            StartCoroutine(PlayBadEndingTimeline());
        }
        else
        {
            Debug.LogError("Bad ending timeline is null");
        }
    }

    IEnumerator PlayGoodEndingTimeline()
    {
        yield return new WaitForSeconds(0.1f);  // Menambahkan sedikit delay
        Debug.Log("Playing Good Ending Timeline");
        goodEndingTimeline.Play();
    }

    IEnumerator PlayBadEndingTimeline()
    {
        yield return new WaitForSeconds(0.1f);  // Menambahkan sedikit delay
        Debug.Log("Playing Bad Ending Timeline");
        badEndingTimeline.Play();
    }

    void Update()
    {
    }
}
