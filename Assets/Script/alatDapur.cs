using UnityEngine;

public class alatDapur : MonoBehaviour
{
    public int durability = 100;
    public int currentDurability;
    void Start()
    {
        currentDurability = durability;
    }
    public void kurang()
    {
        if(currentDurability > 0)
        {
            currentDurability -= 10;
        }
    }
    public void ganti()
    {
        currentDurability = durability;
    }
}
