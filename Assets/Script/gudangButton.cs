using UnityEngine;
using UnityEngine.UI;

public class gudangButton : MonoBehaviour
{
    private bool gudangActive = false;
    public Sprite gudangOn;
    public Sprite gudangOff;
    public Image image;

    public void geser(){
        GameObject gudang = GameObject.Find("gudang");
        if(gudangActive){
            gudang.transform.position += Vector3.right * 460;
            gudangActive = false;
            image.sprite = gudangOff;
        }else{
            gudang.transform.position += Vector3.left * 460;
            gudangActive = true;
            image.sprite = gudangOn;
        }
    }
}
