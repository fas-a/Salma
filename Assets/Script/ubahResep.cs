using UnityEngine;
using UnityEngine.UI;

public class ubahResep : MonoBehaviour
{
    private Image targetImage;
    private short currentPage;
    private short maxPage = 2;

    void Start()
    {
        currentPage = 1;
        targetImage = GetComponent<Image>();
    }
    
    public void nextImage()
    {   
        if(currentPage < maxPage){
            currentPage++;
            string name = "Image/resep-jamu-" + currentPage;
            Sprite newSprite = Resources.Load<Sprite>(name);
            targetImage.sprite = newSprite;
        }
    }

    public void backImage()
    {   
        if(currentPage > 1){
            currentPage--;
            string name = "Image/(dummy)konten-resep-" + currentPage ;
            Sprite newSprite = Resources.Load<Sprite>(name);
            targetImage.sprite = newSprite;
        }
    }
}