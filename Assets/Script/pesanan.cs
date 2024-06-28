using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Pesanan : MonoBehaviour, IDataPersistence
{
    public List<GameObject> jamuItem; // Daftar lengkap jamu
    public Transform ordersContainer;
    public customerSpawner customerSpawner;
    private float doubleMoneyDuration = 180f;
    private List<Vector2> gridPositions;
    private List<ItemPesanan> activeOrders;
    private float timeFreezeEndTime;
    public stageDay stageDayScript;
    public Image doubleMoneyBadge;
    private List<GameObject> unlockedJamuItems; // Daftar jamu yang sudah terbuka
    private List<GameObject> weightedJamuItems; // Daftar weighted jamu

    private int baseSpawnDelay, spawnDelay, jumlahJamuSederhana, jumlahJamuKunyitAsam, jumlahJamuBerasKencur, jumlahJamuPahitan, jumlahJamuTemulawak;
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
                baseSpawnDelay = 25; // Interval antar pesanan lebih lama
                break;
            case GamePersistenceManager.DifficultyLevel.Medium:
                baseSpawnDelay = 20; // Interval antar pesanan lebih cepat
                break;
            case GamePersistenceManager.DifficultyLevel.Hard:
                baseSpawnDelay = 15; // Interval antar pesanan sangat cepat
                break;
        }
        spawnDelay = baseSpawnDelay;
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
            // Jika tidak ada item di weightedJamuItems, gunakan item default pertama
            if (weightedJamuItems.Count == 0)
            {
                Debug.LogError("No items to spawn!");
                yield break;
            }

            GameObject randomProduct = weightedJamuItems[Random.Range(0, weightedJamuItems.Count)];
            GameObject newOrder = Instantiate(randomProduct, ordersContainer);
            newOrder.transform.SetParent(ordersContainer, false);

            ItemPesanan newItemPesanan = newOrder.GetComponent<ItemPesanan>();
            newItemPesanan.pesanan = this;
            activeOrders.Add(newItemPesanan);

            bool isTimeFreeze = Random.value < 0.02f;
            bool isDoubleMoney = !isTimeFreeze && Random.value < 0.02f;

            Debug.Log("New Order: " + newItemPesanan.name);

            int index = gridPositions.Count > 0 ? Random.Range(0, gridPositions.Count) : 0;
            Vector2 gridPos = gridPositions[index];
            RectTransform orderRect = newOrder.GetComponent<RectTransform>();
            orderRect.anchoredPosition = gridPos;

            GameObject customer = customerSpawner.SpawnCustomer();
            newItemPesanan.relatedCustomer = customer;
            customer.GetComponent<Customer>().SetLinkedOrder(newItemPesanan);

            // Tentukan spawnDelay berdasarkan hari saat ini
            int currentDay = stageDayScript.GetCurrentDay();
            spawnDelay += (currentDay / 6) * 2; // Bertambah 5 detik setiap 10 hari

            Debug.Log("Waiting for " + spawnDelay + " seconds before spawning next order.");
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public bool GetMatchingOrder(string itemTag)
    {
        foreach (ItemPesanan order in activeOrders)
        {
            if (order != null && order.IsMatch(itemTag))
            {
                if (order.hasTimeFreeze)
                {
                    stageDayScript.ActivateTimeFreeze();
                }
                if (order.hasDoubleMoney)
                {
                    StartCoroutine(ActivateDoubleMoneyBadge());
                }

                IncrementOrderCount(itemTag);
                order.CompleteOrder(true);
                RemoveOrder(order);
                return true;
            }
        }
        return false;
    }

    private IEnumerator ActivateDoubleMoneyBadge()
    {
        doubleMoneyBadge.gameObject.SetActive(true); // Aktifkan badge
        yield return new WaitForSeconds(doubleMoneyDuration);
        doubleMoneyBadge.gameObject.SetActive(false); // Nonaktifkan badge setelah Double Money berakhir
    }

    private void IncrementOrderCount(string itemTag)
    {
        switch (itemTag)
        {
            case "jamuSederhana":
                jumlahJamuSederhana++;
                Debug.Log("Jumlah Jamu Sederhana: " + jumlahJamuSederhana);
                break;
            case "jamuKunyitAsam":
                jumlahJamuKunyitAsam++;
                Debug.Log("Jumlah Jamu Kunyit Asam: " + jumlahJamuKunyitAsam);
                break;
            case "jamuBerasKencur":
                jumlahJamuBerasKencur++;
                Debug.Log("Jumlah Jamu Beras Kencur: " + jumlahJamuBerasKencur);
                break;
            case "jamuPahitan":
                jumlahJamuPahitan++;
                Debug.Log("Jumlah Jamu Pahitan: " + jumlahJamuPahitan);
                break;
            case "jamuTemulawak":
                jumlahJamuTemulawak++;
                Debug.Log("Jumlah Jamu Temulawak: " + jumlahJamuTemulawak);
                break;
            default:
                Debug.LogWarning("Tag item tidak dikenal: " + itemTag);
                break;
        }
    }

    public void LoadData(GameData data)
    {
        this.jumlahJamuSederhana = data.jumlahJamuSederhana;
        this.jumlahJamuKunyitAsam = data.jumlahJamuKunyitAsam;
        this.jumlahJamuBerasKencur = data.jumlahJamuBerasKencur;
        this.jumlahJamuPahitan = data.jumlahJamuPahitan;
        this.jumlahJamuTemulawak = data.jumlahJamuTemulawak;
        this.spawnDelay = data.spawnDelay;
    }

    public void SaveData(GameData data)
    {
        data.jumlahJamuSederhana = this.jumlahJamuSederhana;
        data.jumlahJamuKunyitAsam = this.jumlahJamuKunyitAsam;
        data.jumlahJamuBerasKencur = this.jumlahJamuBerasKencur;
        data.jumlahJamuPahitan = this.jumlahJamuPahitan;
        data.jumlahJamuTemulawak = this.jumlahJamuTemulawak;
        data.spawnDelay = this.spawnDelay;
    }

    public void RemoveOrder(ItemPesanan order)
    {
        if (order != null && activeOrders.Contains(order))
        {
            activeOrders.Remove(order);
        }
    }

    public void ClearActiveOrders()
    {
        foreach (ItemPesanan order in activeOrders)
        {
            if (order != null)
            {
                if (order.relatedCustomer != null)
                {
                    Destroy(order.relatedCustomer);
                }
                Destroy(order.gameObject);
            }
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
        GameData gameData = GamePersistenceManager.instance.GetGameData();
        GamePersistenceManager.DifficultyLevel difficulty = gameData.difficulty;

        // Tentukan indeks unlocking berdasarkan tingkat kesulitan
        int unlockIndexForWeighting = 0;
        switch (difficulty)
        {
            case GamePersistenceManager.DifficultyLevel.Easy:
                unlockIndexForWeighting = 1; // Mungkin item pertama dan kedua lebih sering muncul
                break;
            case GamePersistenceManager.DifficultyLevel.Medium:
                unlockIndexForWeighting = 3; // Mungkin item ketiga lebih sering muncul
                break;
            case GamePersistenceManager.DifficultyLevel.Hard:
                unlockIndexForWeighting = 4; // Mungkin item keempat dan kelima lebih sering muncul
                break;
        }

        // Tambahkan item ke weightedJamuItems sesuai dengan bobot berdasarkan unlockIndexForWeighting
        foreach (GameObject jamu in unlockedJamuItems)
        {
            int weight = 1; // Default weight

            // Sesuaikan weight berdasarkan unlockIndex
            if (unlockedJamuItems.IndexOf(jamu) <= unlockIndexForWeighting)
            {
                weight = 5; // Bobot lebih tinggi untuk item yang berada dalam unlockIndexForWeighting
            }

            // Tambahkan item ke daftar weighted berdasarkan weight
            for (int i = 0; i < weight; i++)
            {
                weightedJamuItems.Add(jamu);
            }
        }

        // Jika hanya satu item yang terbuka, pastikan hanya item itu yang ditambahkan
        if (weightedJamuItems.Count == 0 && unlockedJamuItems.Count > 0)
        {
            weightedJamuItems.Add(unlockedJamuItems[0]);
        }

        // Debugging log
        Debug.Log("Updated Weighted Jamu Items Count: " + weightedJamuItems.Count);
        foreach (var item in weightedJamuItems)
        {
            Debug.Log("Item: " + item.name);
        }
    }
}
