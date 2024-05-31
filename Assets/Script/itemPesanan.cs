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
        //Debug.Log(value);
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
        //Debug.Log(currentValue);
        currentValue -= 0.2f;
        SetProgress(currentValue);
    }

    void CompleteOrder()
    {
        if (OnOrderCompleted != null)
        {
            OnOrderCompleted(hargaJamu);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        DecreaseProgress();
    }
}
