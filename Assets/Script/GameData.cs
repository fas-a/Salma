using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int day, money, expenses, grossProfit, orderCompleted, galon, halaman;
    public float time;
    public int spawnDelay;
    public int jumlahJamuSederhana, jumlahJamuKunyitAsam, jumlahJamuBerasKencur, jumlahJamuPahitan, jumlahJamuTemulawak;
    public GamePersistenceManager.DifficultyLevel difficulty;
    public SerializableDictionary<string, int> slotItems, alat;
    public List<int> unlockedJamuIndexes; // Menyimpan indeks jamu yang telah dibuka

    public GameData()
    {
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
        this.halaman = 0;
        this.difficulty = GamePersistenceManager.DifficultyLevel.Easy; // Default difficulty
        this.slotItems = new SerializableDictionary<string, int>();
        this.alat = new SerializableDictionary<string, int>();
        this.galon = 55;
        this.unlockedJamuIndexes = new List<int>(); // Defaultnya kosong
    }
}
