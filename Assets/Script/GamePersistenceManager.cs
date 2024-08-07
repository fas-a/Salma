using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GamePersistenceManager : MonoBehaviour, IDataPersistence
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static GamePersistenceManager instance { get; private set; }
    private bool isNewGame = true;

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

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
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        isNewGame = PlayerPrefs.GetInt("isNewGame", 0) == 1;

        if (isNewGame)
        {
            NewGame();
        }
        else
        {
            LoadGame();
        }
    }

    public void StartNewGame(DifficultyLevel difficulty)
    {
        isNewGame = true;
        PlayerPrefs.SetInt("isNewGame", 1);
        PlayerPrefs.SetInt("difficulty", (int)difficulty);
        SceneManager.LoadScene(2);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        gameData.difficulty = (DifficultyLevel)PlayerPrefs.GetInt("difficulty", 0); // Load difficulty level
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        
        Debug.Log("new day count = " + gameData.day);
        Debug.Log("new money count = " + gameData.money);
        Debug.Log("new time count = " + gameData.time);
        Debug.Log("new difficulty count = " + gameData.difficulty);
        Debug.Log("new orderCompleted count = " + gameData.orderCompleted);
    }

    public void LoadEndingScene()
    {
        SceneManager.LoadScene(4);
    }

    public void ContinueGame()
    {
        isNewGame = false;
        PlayerPrefs.SetInt("isNewGame", 0);
        SceneManager.LoadScene(3);
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("Tidak ada data ditemukan.Memulai game baru.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Load day count = " + gameData.day);
        Debug.Log("Load money count = " + gameData.money);
        Debug.Log("Load time count = " + gameData.time);
        Debug.Log("Load difficulty = " + gameData.difficulty);
        Debug.Log("Load orderCompletedcount = " + gameData.orderCompleted);
    }

    public void SaveGame()
    {
        if (dataPersistenceObjects == null || dataPersistenceObjects.Count == 0)
        {
            Debug.LogWarning("dataPersistenceObjects is null or empty in SaveGame().");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        Debug.Log("Saved day count = " + gameData.day);
        Debug.Log("Saved money count = " + gameData.money);
        Debug.Log("Saved time count = " + gameData.time);
        Debug.Log("Saved difficulty = " + gameData.difficulty);
        Debug.Log("Saved orderCompleted count = " + gameData.orderCompleted);

        dataHandler.Save(gameData);
    }

    public void LoadData(GameData data)
    {
        this.gameData = data;
    }

    public void SaveData(GameData data)
    {
        data.difficulty = this.gameData.difficulty;
    }

    public GameData GetGameData()
    {
        return gameData;
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
