using UnityEngine;

public class gudang : MonoBehaviour
{
    public GameObject content;
    public void hide()
    {
        gameObject.transform.localScale = new Vector3(1, 0, 1);
    }
    public void show()
    {
        gameObject.transform.localScale = Vector3.one;
        content.transform.position = new Vector3(0, 0, 0);
    }
}
