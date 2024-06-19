using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slotItem : MonoBehaviour
{
    public TMP_Text text;
    public GameObject item;
    public GameObject slot;
    public int stok = 10;

    void start()
    {
        
    }
    public void add(int amount)
    {
        stok += amount;
        text.text = stok.ToString();
    }
    public void remove()
    {
        stok--;
        text.text = stok.ToString();
    }
    public void addBag()
    {
        if(stok > 0){
            bool ada = slot.GetComponent<tasContainer>().cari(item.name);
            int contain = slot.GetComponent<tasContainer>().hitungList();
            if (ada)
            {
                slot.GetComponent<tasContainer>().tambah(item.name);
            }
            else
            {
                GameObject i = Instantiate(item, slot.transform.GetChild(contain), false);
                slot.GetComponent<tasContainer>().spawn(i);
            }
            remove();
        }
        
    }
}
