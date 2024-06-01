using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tasContainer : MonoBehaviour
{
    public List<GameObject> items;

    void start()
    {
        items = new List<GameObject>();
    }

    public void spawn(GameObject item)
    {
        items.Add(item);
        // GameObject foundItem = items.Find(i => i.name == "kunyit Variant(Clone)");
        // Debug.Log("Item added to container");
        // if (foundItem != null)
        // {
        //     Debug.Log("Item ditemukan: " + foundItem.name);
        // }
    }

    public void tambah(string itemName)
    {
        GameObject foundItem = items.Find(i => i.name == itemName+"(Clone)");
        Debug.Log("Item found: " + foundItem.name);
        Transform jumlah = foundItem.transform.Find("jumlah");
        TMP_Text textComponent = jumlah.GetComponent<TMP_Text>();
        int currentAmount = int.Parse(textComponent.text);
        textComponent.text = (currentAmount + 1).ToString();
    }

    public void kurang(string itemName)
    {
        GameObject foundItem = items.Find(i => i.name == itemName);
        Debug.Log("Item found: " + foundItem.name);
        Transform jumlah = foundItem.transform.Find("jumlah");
        TMP_Text textComponent = jumlah.GetComponent<TMP_Text>();
        int currentAmount = int.Parse(textComponent.text);
        if (currentAmount > 1)
        {
            textComponent.text = (currentAmount - 1).ToString();
        }
        else
        {
            items.Remove(foundItem);
            Destroy(foundItem);
            maju();
        }
    }

    public bool cari(string itemName)
    {
        GameObject foundItem = items.Find(i => i.name == itemName+"(Clone)");
        if (foundItem != null)
        {
            return true;
        }
        return false;
    }

    public int hitungList()
    {
        Debug.Log("Jumlah item: " + items.Count);
        return items.Count;
    }

    private void maju()
    {
        Transform newParent;
        for(int i = 0; i < items.Count; i++)
        {
            newParent = transform.GetChild(i);
            items[i].transform.SetParent(newParent);
            items[i].transform.localPosition = Vector3.zero;
        }
    }
}
