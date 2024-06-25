using UnityEngine;
using System.Collections.Generic;

public class galon : MonoBehaviour
{
    public int durability = 55;
    public int currentDurability;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;
    void Start()
    {
        currentDurability = durability;
    }
    public void kurang()
    {
        if(currentDurability > 0)
        {
            currentDurability -= 11;
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
}
