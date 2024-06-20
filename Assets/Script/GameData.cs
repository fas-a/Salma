using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int day;
    public int money;
    public int orderCompleted;
    public float time;
    // public SerializableDictionary<string, int> slotItems;

    public GameData() {
        this.day = 1;
        this.money = 10000;
        this.time = 360;
        this.orderCompleted = 0;
        // this.slotItems = new SerializableDictionary<string, int>();
    }
}
