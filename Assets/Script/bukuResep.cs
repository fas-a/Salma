using UnityEngine;

public class bukuResep : MonoBehaviour
{
    public GameObject halaman;
    private bool tutup = true;

    public void bukaTutup(){
        if(tutup){
            halaman.SetActive(true);
            tutup = false;
        }else{
            halaman.SetActive(false);
            tutup = true;
        }
    }


}
