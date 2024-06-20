using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GamePersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static GamePersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ditemukan lebih dari satu Game Persistence Manager di scene");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Start() {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        if (this.gameData != null)
        {
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        if (this.gameData != null)
        {
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("new day count = " + gameData.day);
        Debug.Log("new money count = " + gameData.money);
        Debug.Log("new time count = " + gameData.time);
        Debug.Log("new orderCompleted count = " + gameData.orderCompleted);
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("Tidak ada data ditemukan. Memulai game baru.");
            NewGame();
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Load day count = " + gameData.day);
        Debug.Log("Load money count = " + gameData.money);
        Debug.Log("Load time count = " + gameData.time);
        Debug.Log("Load orderCompleted count = " + gameData.orderCompleted);
    }

    public void SaveGame()
    {
        if (gameData == null)
        {
            Debug.LogWarning("gameData is null in SaveGame(). Initializing new gameData.");
            NewGame();
        }

        if (dataPersistenceObjects == null || dataPersistenceObjects.Count == 0)
        {
            Debug.LogWarning("dataPersistenceObjects is null or empty in SaveGame().");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj != null)
            {
                dataPersistenceObj.SaveData(gameData);
            }
            else
            {
                Debug.LogWarning("Found a null dataPersistenceObject in SaveGame().");
            }
        }

        Debug.Log("Saved day count = " + gameData.day);
        Debug.Log("Saved money count = " + gameData.money);
        Debug.Log("Saved time count = " + gameData.time);
        Debug.Log("Saved orderCompleted count = " + gameData.orderCompleted);

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
