using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Add this directive for SceneManager

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
            orderSpawner.ClearActiveOrders();
            orderSpawner.SpawnOrders();
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

    public void LoadData(GameData data) {
        this.hari = data.day;
        this.waktuTersisa = data.time;
        UpdateDayText();
    }

    public void SaveData(GameData data) {
        data.day = this.hari;
        data.time = this.waktuTersisa;
    }

    public void ResetData(GameData data) {
        data.day = 1;
        data.time = 360;
        this.hari = data.day;
        this.waktuTersisa = data.time;    
    }
}
