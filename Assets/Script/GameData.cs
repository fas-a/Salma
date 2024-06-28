using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int day, money, expenses, grossProfit, orderCompleted;
    public float time;
    public int spawnDelay;
    public int jumlahJamuSederhana, jumlahJamuKunyitAsam, jumlahJamuBerasKencur, jumlahJamuPahitan, jumlahJamuTemulawak;
    public GamePersistenceManager.DifficultyLevel difficulty;
    public SerializableDictionary<string, int> slotItems;

    public GameData() {
        this.day = 1;
        this.money = 10000;
        this.time = 360;
        this.expenses = 0;
        this.grossProfit = 10000;
        this.orderCompleted = 0;
        this.jumlahJamuSederhana = 0;
        this.jumlahJamuBerasKencur = 0;
        this.jumlahJamuKunyitAsam = 0;
        this.jumlahJamuPahitan = 0;
        this.jumlahJamuTemulawak = 0;
        this.difficulty = GamePersistenceManager.DifficultyLevel.Easy; // Default difficulty
        this.slotItems = new SerializableDictionary<string, int>();
    }
}
