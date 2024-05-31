using UnityEngine;
using UnityEngine.EventSystems;

public class tas : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _draggingPlane;
    private RectTransform _draggingItem;

    void Start()
    {
        _draggingPlane = transform.parent.GetComponent<RectTransform>();
        _draggingItem = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _draggingItem.SetAsLastSibling(); // Bring the item to the front
    }

    public void OnDrag(PointerEventData eventData)
    {
        _draggingItem.anchoredPosition += eventData.delta / _draggingPlane.localScale.x; // Move with mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InstantiateItemInWorld();
            
        RemoveFromInventory();
        
    }

    private void InstantiateItemInWorld()
    {
        // Instantiate the item in the game world at the drop position
        Vector3 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dropPosition.z = 0;
        Instantiate(Resources.Load<GameObject>("Prefab/kunyit"), dropPosition, Quaternion.identity);
        Debug.Log("Item instantiated in the world at position: " + dropPosition);
    }

    private void RemoveFromInventory()
    {
        // Remove the item from the inventory system
        // Implement your inventory management logic here
    }
}
