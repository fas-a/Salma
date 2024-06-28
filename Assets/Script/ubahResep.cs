using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ubahResep : MonoBehaviour, IDataPersistence
{
    public Image targetImage;
    public List<Sprite> resepJamu;
    public int currentPage = 0;
    public int maxPage;

    void Start()
    {
        
    }
    
    public void nextImage()
    {   
        if(currentPage < maxPage){
            currentPage++;
            targetImage.sprite = resepJamu[currentPage];
        }
    }

    public void backImage()
    {   
        if(currentPage > 0){
            currentPage--;
            targetImage.sprite = resepJamu[currentPage];
        }
    }

    public void add()
    {
        maxPage++;
    }

    public void LoadData(GameData data)
    {
        maxPage = data.halaman;
    }

    public void SaveData(GameData data)
    {
        data.halaman = maxPage;
    }
}