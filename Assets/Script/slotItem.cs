using UnityEngine;

public class slotItem : MonoBehaviour
{
    public GameObject prefab;

    public void add()
    {
        GameObject newObject = Instantiate(prefab);
    }
}
