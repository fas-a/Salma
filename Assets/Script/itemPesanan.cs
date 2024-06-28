using UnityEngine;
using UnityEngine.UI;

public class ItemPesanan : MonoBehaviour
{
    public delegate void OrderCompleted(int harga);
    public static event OrderCompleted OnOrderCompleted;
    public string requiredItemTag;
    public Slider progressBar;
    public int hargaJamu;
    public Pesanan pesanan;
    public GameObject relatedCustomer;
    public bool hasTimeFreeze, hasDoubleMoney;
    public Image timeFreezeBadge, doubleMoneyBadge;
    private float decreaseInterval = 1f; // Interval untuk mengurangi progress, dalam detik

    void Start()
    {
        timeFreezeBadge.gameObject.SetActive(hasTimeFreeze);
        doubleMoneyBadge.gameObject.SetActive(hasDoubleMoney);
        InvokeRepeating("DecreaseProgress", 0f, decreaseInterval);
    }

    public void SetProgress(float value)
    {
        progressBar.value = value;
        if (value <= 0)
        {
            CompleteOrder(false);
        }
    }

    void DecreaseProgress()
    {
        float currentValue = progressBar.value;
        currentValue -= decreaseInterval;
        SetProgress(currentValue);
    }

    public void CompleteOrder(bool serahkan)
    {
        if (serahkan)
        {
            int finalPrice = hargaJamu;
            if (hasDoubleMoney)
            {
                finalPrice *= 2;
            }
            OnOrderCompleted?.Invoke(finalPrice);
        }

        if (relatedCustomer != null)
        {
            Customer customerComponent = relatedCustomer.GetComponent<Customer>();
            if (customerComponent != null)
            {
                customerComponent.CompleteOrder();
            }
        }

        if (this != null && gameObject != null)
        {
            Destroy(gameObject);
        }
    }


    public bool IsMatch(string itemTag)
    {
        return requiredItemTag == itemTag;
    }
}
