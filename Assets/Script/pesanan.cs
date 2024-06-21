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
    private List<Vector2> gridPositions;
    private List<ItemPesanan> activeOrders;
    private int jumlahJamuSederhana;
    private int jumlahJamuKunyitAsam;
    private int jumlahJamuBerasKencur;
    private int jumlahJamuPahitan;
    private int jumlahJamuTemulawak;

    void Start()
    {
        InitializeGridPositions();
        activeOrders = new List<ItemPesanan>();
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

            Debug.Log("Jumlah order: " + activeOrders.Count);

            int index = gridPositions.Count > 0 ? Random.Range(0, gridPositions.Count) : 0;
            Vector2 gridPos = gridPositions[index];
            RectTransform orderRect = newOrder.GetComponent<RectTransform>();
            orderRect.anchoredPosition = gridPos;

            // Spawn corresponding customer
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
            Debug.Log("Ini order: " + order);
            if (order.IsMatch(itemTag))
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
                order.CompleteOrder(true);
                RemoveOrder(order);
                return true;
            }
        }
        return false;
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
            Destroy(order.relatedCustomer); // Destroy related customer
            Destroy(order.gameObject);
        }
        activeOrders.Clear();
    }
}
