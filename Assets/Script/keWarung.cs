using UnityEngine;

public class keWarung : MonoBehaviour
{
    public GameObject warung;

    public void buka()
    {
        warung.SetActive(true);
        Time.timeScale = 0f;
    }

    public void tutup()
    {
        warung.SetActive(false);
        Time.timeScale = 1f;
    }
}
