using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UnlockingRecipe : MonoBehaviour
{
    public TMP_Text namaJamu;
    public Image jamuImage; // Image UI yang akan diubah
    public List<Sprite> jamuSprites; // Daftar sprite jamu
    public GameObject modal;
    private int day;

    public void displayRecipe(string jamu, int hari)
    {
        this.day = hari;
        namaJamu.text = jamu;
        switch (jamu)
        {
            case "jamuKunyitAsam":
                jamuImage.sprite = jamuSprites[0];
                break;
            case "jamuBerasKencur":
                jamuImage.sprite = jamuSprites[1];
                break;
            case "jamuPahitan":
                jamuImage.sprite = jamuSprites[2];
                break;
            case "jamuTemulawak":
                jamuImage.sprite = jamuSprites[3];
                break;
            default:
                break;
        }
        modal.SetActive(true);
    }

    public void HideModal()
    {
        modal.SetActive(false); // Nonaktifkan objek popup
    }

    public void OnContinueButtonClick()
    {
        HideModal();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
