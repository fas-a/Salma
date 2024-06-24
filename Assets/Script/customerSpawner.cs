using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class customerSpawner : MonoBehaviour
{
    public List<GameObject> customerPrefabs;
    private List<Vector2> gridPositions;
    public Transform customersContainer;

    void Start()
    {
        InitializeGridPositions();
    }

    void InitializeGridPositions()
    {
        gridPositions = new List<Vector2>();
        GridLayoutGroup gridLayoutGroup = customersContainer.GetComponent<GridLayoutGroup>();

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

    public GameObject SpawnCustomer()
    {
        // spawn random customer prefabs
        GameObject randomCustomer = customerPrefabs[Random.Range(0, customerPrefabs.Count)];
        GameObject newCustomer = Instantiate(randomCustomer, customersContainer);
        newCustomer.transform.SetParent(customersContainer, false);

        int index = gridPositions.Count > 0 ? Random.Range(0, gridPositions.Count) : 0;
        Vector2 gridPos = gridPositions[index];
        RectTransform customerRect = newCustomer.GetComponent<RectTransform>();
        customerRect.anchoredPosition = gridPos;

        return newCustomer;
    }
}
