using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tas : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _draggingPlane;
    private RectTransform _draggingItem;
    private Vector3 _startPosition;
    public GameObject item;
    public bool alat = false;

    void Start()
    {
        _draggingPlane = transform.parent.GetComponent<RectTransform>();
        _draggingItem = GetComponent<RectTransform>();
    }


    public void changeColor(int intensity)
    {
        Image renderer = gameObject.GetComponent<Image>();
        Color color = renderer.color;
        color.a = intensity;
        renderer.color = color;
        Transform nama = transform.Find("nama");
        TMP_Text textComponent = nama.GetComponent<TMP_Text>();   
        color = textComponent.color;
        color.a = intensity;
        textComponent.color = color;
        Transform jumlah = transform.Find("jumlah");
        TMP_Text textComponent2 = jumlah.GetComponent<TMP_Text>();
        color = textComponent2.color;
        color.a = intensity;
        textComponent2.color = color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _draggingItem.SetAsLastSibling(); // Bring the item to the front
        _startPosition = _draggingItem.anchoredPosition; // Store the starting position
        changeColor(0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _draggingItem.anchoredPosition += eventData.delta / _draggingPlane.localScale.x; // Move with mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(alat)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject target = hit.collider.gameObject;
                alatDapur alatDapur = target.GetComponent<alatDapur>();
                if (alatDapur != null)
                {
                    Debug.Log("Item dropped on alat dapur: " + target.name);
                }
            }
        }
        else
        {
            InstantiateItemInWorld();
        }
        
        RemoveFromInventory();
        changeColor(1);
    }

    private void InstantiateItemInWorld()
    {
        // Instantiate the item in the game world at the drop position
        Vector3 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dropPosition.z = 0;
        Instantiate(item, dropPosition, Quaternion.identity);
        Debug.Log("Item instantiated in the world at position: " + dropPosition);
    }

    private void RemoveFromInventory()
    {
        // Remove the item from the inventory system
        // Implement your inventory management logic here
        _draggingItem.anchoredPosition = _startPosition; // Reset the item to its original position
        Transform parent = _draggingItem.parent;
        parent.parent.GetComponent<tasContainer>().kurang(transform.name);
    }
}
