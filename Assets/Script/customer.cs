using UnityEngine;

public class Customer : MonoBehaviour
{
    private ItemPesanan linkedOrder;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize any necessary components or properties
    }

    // Update is called once per frame
    void Update()
    {
        // Any per-frame logic for the customer can go here
    }

    public void SetLinkedOrder(ItemPesanan order)
    {
        linkedOrder = order;
    }

    public void CompleteOrder()
    {
        // Perform any necessary actions when the order is completed
        Destroy(gameObject);
    }
}
