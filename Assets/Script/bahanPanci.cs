using UnityEngine;
using TMPro;

public class bahanPanci : MonoBehaviour
{
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void addBahan(string bahan)
    {
        text.text += bahan + "\n";
    }

    public void clearBahan()
    {
        text.text = "";
    }

    public void showBahan()
    {
        gameObject.SetActive(true);
    }

    public void hideBahan()
    {
        gameObject.SetActive(false);
    }
}
