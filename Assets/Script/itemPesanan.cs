using UnityEngine;
using UnityEngine.UI;

public class itemPesanan : MonoBehaviour
{
    public delegate void OrderCompleted(int harga);
    public static event OrderCompleted OnOrderCompleted;

    public string requiredItemTag;
    public Slider progressBar;
    public int hargaJamu;

    private float decreaseInterval = 1f; // Interval untuk mengurangi progress, dalam detik

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

    void Start()
    {
        InvokeRepeating("DecreaseProgress", 0f, decreaseInterval);
    }

    void DecreaseProgress()
    {
        float currentValue = progressBar.value;
        currentValue -= decreaseInterval;
        SetProgress(currentValue);
    }

    void CompleteOrder()
    {
        OnOrderCompleted?.Invoke(hargaJamu);
        Destroy(gameObject);
    }
}
