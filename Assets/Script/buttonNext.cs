using UnityEngine;
using UnityEngine.UI;

public class buttonNext : MonoBehaviour
{
    public timelineManager timelineManager; // Pastikan di-set melalui Inspector atau di-script

    void Start()
    {
        // Ambil komponen Button dari GameObject ini
        Button button = GetComponent<Button>();

        // Tambahkan listener untuk menanggapi klik
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("Button component tidak ditemukan pada GameObject ini.");
        }
    }

    void OnClick()
    {
        // Pastikan timelineManager sudah di-set
        if (timelineManager != null)
        {
            // Panggil method HandleSignalFromTimeline dengan nama signal yang sesuai
            timelineManager.HandleSignalFromTimeline("NextScene");
        }
        else
        {
            Debug.LogError("timelineManager tidak di-set untuk tombol ini.");
        }
    }
}
