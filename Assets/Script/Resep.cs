using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Resep
{
    public string name;
    public List<string> items;
    public Sprite sprite;

    public Resep(string name, List<string> items, Sprite sprite)
    {
        this.name = name;
        this.items = items;
        this.sprite = sprite;
    }
}
