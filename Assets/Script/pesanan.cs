using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Pesanan : MonoBehaviour, IDataPersistence
{
    public List<GameObject> jamuItem;
    public Transform ordersContainer;
    public customerSpawner customerSpawner; 
    public float spawnDelay;
    private float timeFreezeDuration = 5f; // Durasi Time Freeze
    private float doubleMoneyDuration = 5f; // Durasi Double Money

    private List<Vector2> gridPositions;
    private List<ItemPesanan> activeOrders;
    private bool timeFrozen;
    private float timeFreezeEndTime;

    private int jumlahJamuSederhana;
    private int jumlahJamuKunyitAsam;
    private int jumlahJamuBerasKencur;
    private int jumlahJamuPahitan;
    private int jumlahJamuTemulawak;

    void Start()
    {
        InitializeGridPositions();
        activeOrders = new List<ItemPesanan>();
        timeFrozen = false;
    }

    void Update()
    {
        if (timeFrozen && Time.time > timeFreezeEndTime)
        {
            timeFrozen = false;
        }
    }

    void InitializeGridPositions()
    {
        gridPositions = new List<Vector2>();
        GridLayoutGroup gridLayoutGroup = ordersContainer.GetComponent<GridLayoutGroup>();

        if (gridLayoutGroup != null)
        {
            int columns = gridLayoutGroup.constraintCount;
            Vector2 cellSize = gridLayoutGroup.cellSize;
            Vector2 spacing = gridLayoutGroup.spacing;
            Vector2 startPosition = new Vector2(
                cellSize.x / 2 + spacing.x / 2,
                -cellSize.y / 2 - spacing.y / 2
            );

            for (int x = 0; x < columns; x++)
            {
                Vector2 position = new Vector2(
                    startPosition.x + x * (cellSize.x + spacing.x),
                    startPosition.y
                );
                gridPositions.Add(position);
            }
        }
    }

    public void SpawnOrders()
    {
        StartCoroutine(SpawnOrdersCoroutine());
    }

    IEnumerator SpawnOrdersCoroutine()
    {
        while (true)
        {
            GameObject randomProduct = jamuItem[Random.Range(0, jamuItem.Count)];
            GameObject newOrder = Instantiate(randomProduct, ordersContainer);
            newOrder.transform.SetParent(ordersContainer, false);

            ItemPesanan newItemPesanan = newOrder.GetComponent<ItemPesanan>();
            newItemPesanan.pesanan = this;
            activeOrders.Add(newItemPesanan);

            // Tentukan apakah order ini memiliki power-up
            newItemPesanan.hasTimeFreeze = Random.value < 0.1f; // 10% chance
            newItemPesanan.hasDoubleMoney = Random.value < 0.1f; // 10% chance

            Debug.Log("Jumlah order: " + activeOrders.Count);

            int index = gridPositions.Count > 0 ? Random.Range(0, gridPositions.Count) : 0;
            Vector2 gridPos = gridPositions[index];
            RectTransform orderRect = newOrder.GetComponent<RectTransform>();
            orderRect.anchoredPosition = gridPos;

            GameObject customer = customerSpawner.SpawnCustomer();
            newItemPesanan.relatedCustomer = customer;
            customer.GetComponent<Customer>().SetLinkedOrder(newItemPesanan);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public bool GetMatchingOrder(string itemTag)
    {
        foreach (ItemPesanan order in activeOrders)
        {
            if (order.IsMatch(itemTag))
            {
                if (order.hasTimeFreeze)
                {
                    ActivateTimeFreeze();
                }
                if (order.hasDoubleMoney)
                {
                    StartCoroutine(ActivateDoubleMoney());
                }

                IncrementOrderCount(itemTag);
                order.CompleteOrder(true);
                RemoveOrder(order);
                return true;
            }
        }
        return false;
    }

    private void ActivateTimeFreeze()
    {
        timeFrozen = true;
        timeFreezeEndTime = Time.time + timeFreezeDuration;
    }

    private IEnumerator ActivateDoubleMoney()
    {
        float originalDelay = spawnDelay;
        spawnDelay /= 2;
        yield return new WaitForSeconds(doubleMoneyDuration);
        spawnDelay = originalDelay;
    }

    private void IncrementOrderCount(string itemTag)
    {
        switch (itemTag)
        {
            case "jamuSederhana":
                jumlahJamuSederhana++;
                break;
            case "jamuKunyitAsam":
                jumlahJamuKunyitAsam++;
                break;
            case "jamuBerasKencur":
                jumlahJamuBerasKencur++;
                break;
            case "jamuPahitan":
                jumlahJamuPahitan++;
                break;
            case "jamuTemulawak":
                jumlahJamuTemulawak++;
                break;
            default:
                break;
        }
    }

    public bool IsTimeFrozen()
    {
        return timeFrozen;
    }

    public void LoadData(GameData data)
    {
        this.jumlahJamuSederhana = data.jumlahJamuSederhana;
        this.jumlahJamuKunyitAsam = data.jumlahJamuKunyitAsam;
        this.jumlahJamuBerasKencur = data.jumlahJamuBerasKencur;
    }

    public void SaveData(GameData data)
    {
        data.jumlahJamuSederhana = this.jumlahJamuSederhana;
        data.jumlahJamuKunyitAsam = this.jumlahJamuKunyitAsam;
        data.jumlahJamuBerasKencur = this.jumlahJamuBerasKencur;
    }

    public void RemoveOrder(ItemPesanan order)
    {
        activeOrders.Remove(order);
    }

    public void ClearActiveOrders()
    {
        foreach (ItemPesanan order in activeOrders)
        {
            Destroy(order.relatedCustomer);
            Destroy(order.gameObject);
        }
        activeOrders.Clear();
    }
}
