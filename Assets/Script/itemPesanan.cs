using UnityEngine;
using UnityEngine.UI;

public class itemPesanan : MonoBehaviour
{
    public delegate void OrderCompleted(int harga);
    public static event OrderCompleted OnOrderCompleted;

    public string requiredItemTag;
    public Slider progressBar;
    public int hargaJamu;

    public void SetProgress(float value)
    {
        progressBar.value = value;
        if (value <= 0)
        {
            CompleteOrder();
        }
    }

    public void SetRequiredItemTag(string tagJamu)
    {
        requiredItemTag = tagJamu;
    }

    public void SetHargaJamu(int harga)
    {
        hargaJamu = harga;
    }

    void DecreaseProgress()
    {
        float currentValue = progressBar.value;
        currentValue -= 0.01f;
        SetProgress(currentValue);
    }

    void CompleteOrder()
    {
        OnOrderCompleted?.Invoke(hargaJamu);
        Destroy(gameObject);
    }

    void Update()
    {
        DecreaseProgress();
    }
}
