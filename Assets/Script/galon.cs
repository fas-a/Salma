using UnityEngine;
using System.Collections.Generic;

public class galon : MonoBehaviour, IDataPersistence
{
    public int durability = 55;
    public int currentDurability;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;
    void Start()
    {
        
    }
    public void kurang()
    {
        if(currentDurability > 0)
        {
            currentDurability -= 1;
        }
        if(currentDurability % 11 == 0)
        {
            spriteRenderer.sprite = sprites[currentDurability / 11];
        }
    }
    public void ganti()
    {
        currentDurability = durability;
        spriteRenderer.sprite = sprites[5];
    }

    public void LoadData(GameData data)
    {
        currentDurability = data.galon;
        int index;
        if (currentDurability % 11 == 0)
        {
            index = currentDurability / 11;
        }
        else
        {
            index = currentDurability / 11 + 1;
        }
        spriteRenderer.sprite = sprites[index];
        if (currentDurability <= 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
    public void SaveData(GameData data)
    {
        data.galon = currentDurability;
    }
}
