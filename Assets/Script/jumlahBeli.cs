using UnityEngine;
using TMPro;

public class jumlahBeli : MonoBehaviour
{
    public TMP_Text jumlah;
    public int jumlahBeliItem = 0;
    public int harga;
    public slotItem item;
    public chart keranjang;
    void Start()
    {
        jumlah.text = jumlahBeliItem.ToString();
    }

    public void tambah(){
        jumlahBeliItem++;
        jumlah.text = jumlahBeliItem.ToString();
        keranjang.tambah(item, harga);
        keranjang.warungItem(this);
    }

    public void kurang(){
        if(jumlahBeliItem > 0){
            jumlahBeliItem--;
            jumlah.text = jumlahBeliItem.ToString();
            keranjang.kurang(item, harga);
        }
    }

    public void reset(){
        jumlahBeliItem = 0;
        jumlah.text = jumlahBeliItem.ToString();
    }
}
