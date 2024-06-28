using UnityEngine;

public class alatDapur : MonoBehaviour, IDataPersistence
{
    public int durability;
    public string name;
    public int currentDurability;
    public SpriteRenderer spriteRenderer;
    public Sprite brokenSprite;
    public Sprite normalSprite;
    void Start()
    {
        
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

    public void LoadData(GameData data)
    {
        if (data.alat.TryGetValue(name, out int savedDurability))
        {
            Debug.Log("Load " + name + " " + savedDurability);
            currentDurability = savedDurability;
            if(currentDurability <= 0)
            {
                spriteRenderer.sprite = brokenSprite;
            }
        }
        else
        {
            currentDurability = durability;
            spriteRenderer.sprite = normalSprite;
        }
    }

    public void SaveData(GameData data)
    {
        data.alat[name] = currentDurability;
    }
}
