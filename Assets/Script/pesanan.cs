using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Pesanan : MonoBehaviour, IDataPersistence
{
    public List<GameObject> jamuItem; // Daftar lengkap jamu
    public Transform ordersContainer;
    public customerSpawner customerSpawner;
    public float spawnDelay;

    private float doubleMoneyDuration = 180f;
    private List<Vector2> gridPositions;
    private List<ItemPesanan> activeOrders;
    private float timeFreezeEndTime;
    public stageDay stageDayScript;
    public Image doubleMoneyBadge;
    private List<GameObject> unlockedJamuItems; // Daftar jamu yang sudah terbuka
    private List<GameObject> weightedJamuItems; // Daftar weighted jamu

    private int jumlahJamuSederhana;
    private int jumlahJamuKunyitAsam;
    private int jumlahJamuBerasKencur;
    private int jumlahJamuPahitan;
    private int jumlahJamuTemulawak;
    public UnlockingRecipe popup;

    void Start()
    {
        InitializeGridPositions();
        activeOrders = new List<ItemPesanan>();
        unlockedJamuItems = new List<GameObject> { jamuItem[0] }; // Jamu pertama terbuka di awal

        // Atur tingkat kesulitan
        GameData gameData = GamePersistenceManager.instance.GetGameData();
        SetDifficulty(gameData.difficulty);
        UpdateWeightedJamuItems();
    }

    void SetDifficulty(GamePersistenceManager.DifficultyLevel difficulty)
    {
        switch (difficulty)
        {
            case GamePersistenceManager.DifficultyLevel.Easy:
                spawnDelay = 40f; // Interval antar pesanan lebih lama
                break;
            case GamePersistenceManager.DifficultyLevel.Medium:
                spawnDelay = 30f; // Interval antar pesanan lebih cepat
                break;
            case GamePersistenceManager.DifficultyLevel.Hard:
                spawnDelay = 25f; // Interval antar pesanan sangat cepat
                break;
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
            GameObject randomProduct = weightedJamuItems[Random.Range(0, weightedJamuItems.Count)];
            GameObject newOrder = Instantiate(randomProduct, ordersContainer);
            newOrder.transform.SetParent(ordersContainer, false);

            ItemPesanan newItemPesanan = newOrder.GetComponent<ItemPesanan>();
            newItemPesanan.pesanan = this;
            activeOrders.Add(newItemPesanan);

            newItemPesanan.hasTimeFreeze = Random.value < 0.1f;
            newItemPesanan.hasDoubleMoney = Random.value < 0.1f;

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
                    stageDayScript.ActivateTimeFreeze();
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

    private IEnumerator ActivateDoubleMoney()
    {
        doubleMoneyBadge.gameObject.SetActive(true); // Aktifkan badge
        float originalDelay = spawnDelay;
        spawnDelay /= 2;
        yield return new WaitForSeconds(doubleMoneyDuration);
        spawnDelay = originalDelay;
        doubleMoneyBadge.gameObject.SetActive(false); // Nonaktifkan badge setelah Double Money berakhir
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

    public void UnlockJamu(int day)
    {
        int unlockIndex = day / 6;
        if (unlockIndex < jamuItem.Count && !unlockedJamuItems.Contains(jamuItem[unlockIndex]))
        {
            unlockedJamuItems.Add(jamuItem[unlockIndex]);
            UpdateWeightedJamuItems();
            Debug.Log("Unlocked Jamu: " + jamuItem[unlockIndex].name);
            popup.displayRecipe(jamuItem[unlockIndex].name, day);
        }
    }

    void UpdateWeightedJamuItems()
    {
        weightedJamuItems = new List<GameObject>();

        foreach (GameObject jamu in unlockedJamuItems)
        {
            // Default weight
            int weight = 1;

            if (GamePersistenceManager.instance.GetGameData().difficulty == GamePersistenceManager.DifficultyLevel.Easy)
            {
                if (jamu == jamuItem[0] || jamu == jamuItem[1]) // Jamu index 0-1 lebih sering muncul di easy
                {
                    weight = 5;
                }
            }
            else if (GamePersistenceManager.instance.GetGameData().difficulty == GamePersistenceManager.DifficultyLevel.Medium)
            {
                if (jamu == jamuItem[3]) // Jamu index 3 lebih sering muncul di medium
                {
                    weight = 5;
                }
            }
            else if (GamePersistenceManager.instance.GetGameData().difficulty == GamePersistenceManager.DifficultyLevel.Hard)
            {
                if (jamu == jamuItem[4] || jamu == jamuItem[5]) // Jamu index 4-5 lebih sering muncul di hard
                {
                    weight = 5;
                }
            }

            // Tambahkan jamu ke daftar weighted berdasarkan weight
            for (int i = 0; i < weight; i++)
            {
                weightedJamuItems.Add(jamu);
            }
        }
    }
}
