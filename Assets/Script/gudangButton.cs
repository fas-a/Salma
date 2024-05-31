using UnityEngine;

public class gudangButton : MonoBehaviour
{
    private bool gudangActive = false;

    public void geser(){
        GameObject gudang = GameObject.Find("gudang");
        if(gudangActive){
            gudang.transform.position += Vector3.right * 365;
            gudangActive = false;
        }else{
            gudang.transform.position += Vector3.left * 365;
            gudangActive = true;
        }
    }
}
