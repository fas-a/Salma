using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class stageDay : MonoBehaviour, IDataPersistence
{
    public int hari;
    public TMP_Text teksHari;
    public Progress progressScript;
    public Pesanan orderSpawner;
    public float durasiHari = 360f;
    private float waktuTersisa;
    private int jamMulai = 7; // Jam mulai pukul 7 pagi
    public resultPopUp popup;
    private bool timeFrozen;
    private float timeFreezeEndTime;
    public Image timeFreezeBadge;

    void Start()
    {
        MulaiHariBaru();
        UpdateDayText();
        InvokeRepeating("HitungMundurWaktu", 0f, 1f);
    }

    public void LoadData(GameData data)
    {
        this.hari = data.day;
        this.waktuTersisa = data.time;
        Debug.Log("waktu" + waktuTersisa);
        progressScript.UpdateTimer(waktuTersisa, durasiHari, jamMulai);
    }

    public void SaveData(GameData data)
    {
        data.day = this.hari;
        data.time = this.waktuTersisa;
    }

    void HitungMundurWaktu()
    {
        if (timeFrozen && Time.time > timeFreezeEndTime)
        {
            timeFrozen = false;
        }

        if (!timeFrozen && waktuTersisa > 0)
        {
            waktuTersisa -= 1f;
            if (progressScript != null)
            {
                progressScript.UpdateTimer(waktuTersisa, durasiHari, jamMulai);
            }
            else
            {
                Debug.LogError("progressScript is not assigned in HitungMundurWaktu!");
            }
        }
        else if (waktuTersisa <= 0)
        {
            HariSelesai();
        }
    }

    public void MulaiHariBaru()
    {
        if (waktuTersisa == 0)
        {
            waktuTersisa = durasiHari;
        }

        if (orderSpawner != null)
        {
            orderSpawner.ClearActiveOrders();
            orderSpawner.SpawnOrders();

            orderSpawner.UnlockJamu(hari); // membuka jamu baru setiap 6 hari
        }
        else
        {
            Debug.LogError("orderSpawner is not assigned in MulaiHariBaru!");
        }
    }

    public void HariBerikutnya()
    {
        if (hari < 30)
        {
            hari++;
            UpdateDayText();
            MulaiHariBaru();
        }
        else
        {
            LoadEndingScene();
        }
    }

    public void HariSelesai()
    {
        popup.displayResult(hari, progressScript.GetJumlahPesanan());
    }

    public void LoadEndingScene()
    {
        SceneManager.LoadScene("endingScene", LoadSceneMode.Single);
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

    public void ActivateTimeFreeze()
    {
        timeFrozen = true;
        timeFreezeEndTime = Time.time + 60f; // Membekukan waktu selama 60 detik
        timeFreezeBadge.gameObject.SetActive(true); // Aktifkan badge
        StartCoroutine(DeactivateTimeFreezeBadge()); // Mulai coroutine untuk menonaktifkan badge
    }

    private IEnumerator DeactivateTimeFreezeBadge()
    {
        yield return new WaitForSeconds(60f); // Tunggu 60 detik (atau durasi yang diinginkan)
        timeFreezeBadge.gameObject.SetActive(false); // Nonaktifkan badge
    }
}
