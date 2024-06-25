using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ubahResep : MonoBehaviour
{
    private Image targetImage;
    public List<Sprite> resepJamu;
    private short currentPage;
    private short maxPage;

    void Start()
    {
        currentPage = 1;
        targetImage = GetComponent<Image>();
    }
    
    public void nextImage()
    {   
        if(currentPage < maxPage){
            currentPage++;
            targetImage.sprite = resepJamu[currentPage - 1];
        }
    }

    public void backImage()
    {   
        if(currentPage > 1){
            currentPage--;
            targetImage.sprite = resepJamu[currentPage - 1];
        }
    }

    public void add()
    {
        maxPage++;
    }
}