using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class chart : MonoBehaviour
{
    public int totalHarga = 0;
    public TMP_Text text;
    public Dictionary<slotItem, int> items = new Dictionary<slotItem, int>();
    public List<jumlahBeli> warung = new List<jumlahBeli>();
    public Progress proses;
    public TMP_Text tabungan;
    void Start()
    {
        tabungan.text = "Rp " + proses.getUang().ToString();
    }
    public void warungItem(jumlahBeli item)
    {
        warung.Add(item);
    }

    public void tambah(slotItem item, int harga)
    {
        totalHarga += harga;
        if (items.ContainsKey(item))
        {
            items[item] += 1;
        }
        else
        {
            items.Add(item, 1);
        }
        text.text = "Rp " + totalHarga.ToString();
    }

    public void kurang(slotItem item, int harga)
    {
        if (items.ContainsKey(item))
        {
            items[item] -= 1;
            if (items[item] == 0)
            {
                items.Remove(item);
            }
            totalHarga -= harga;
            text.text = "Rp " + totalHarga.ToString();
        }
    }

    public void beli()
    {
        if(proses.getUang() >= totalHarga){
            proses.beli(totalHarga);
            foreach (KeyValuePair<slotItem, int> item in items)
            {
                item.Key.add(item.Value);
            }
            items.Clear();
            foreach (jumlahBeli item in warung)
            {
                item.reset();
            }
            warung.Clear();
            totalHarga = 0;
            text.text = "Rp " + totalHarga.ToString();
            tabungan.text = "Rp " + proses.getUang().ToString();
        }
    }
}
