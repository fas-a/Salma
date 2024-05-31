using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OrderSpawner : MonoBehaviour
{
    public List<GameObject> jamuItem;
    public Transform ordersContainer;
    public int jumlahPesanan;
    public float spawnDelay;
    private List<Vector2> gridPositions;

    void Start()
    {
        InitializeGridPositions();
        SpawnOrders();
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

            for (int x = 0; x < jumlahPesanan; x++)
            {
                Vector2 position = new Vector2(
                    startPosition.x + x * (cellSize.x + spacing.x),
                    startPosition.y
                );
                gridPositions.Add(position);
            }
        }
    }

    void SpawnOrders()
    {
        StartCoroutine(SpawnOrdersCoroutine());
    }

    IEnumerator SpawnOrdersCoroutine()
    {
        for (int i = 0; i < jumlahPesanan; i++)
        {
            GameObject randomProduct = jamuItem[Random.Range(0, jamuItem.Count)];
            GameObject newOrder = Instantiate(randomProduct, ordersContainer);
            newOrder.transform.SetParent(ordersContainer, false);

            Vector2 gridPos = gridPositions[i];
            RectTransform orderRect = newOrder.GetComponent<RectTransform>();

            orderRect.anchoredPosition = gridPos;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}