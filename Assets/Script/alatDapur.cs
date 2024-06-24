using UnityEngine;

public class alatDapur : MonoBehaviour
{
    public int durability = 100;
    public int currentDurability;
    public SpriteRenderer spriteRenderer;
    public Sprite brokenSprite;
    public Sprite normalSprite;
    void Start()
    {
        currentDurability = durability;
    }
    public void kurang()
    {
        if(currentDurability > 0)
        {
            currentDurability -= 1;
            if(currentDurability <= 0)
            {
                spriteRenderer.sprite = brokenSprite;
            }
        }
    }
    public void ganti()
    {
        currentDurability = durability;
        spriteRenderer.sprite = normalSprite;
    }
}
