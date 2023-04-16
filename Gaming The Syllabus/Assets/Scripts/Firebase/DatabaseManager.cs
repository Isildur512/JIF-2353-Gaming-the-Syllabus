using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.IO;

using FirebaseBridge;

public class DatabaseManager : Singleton<DatabaseManager>
{
    public static Action OnRiddlesLoaded;
    public static Action OnEnemiesLoaded;

    public static bool RiddlesHaveBeenLoaded { get; private set; }
    public static bool EnemiesHaveBeenLoaded { get; private set; }

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        // TODO: We need to load all the enemy and riddle files eventually. This is kind of a pain since there isn't just a "download folder" option.
        _instance.StartCoroutine(LoadRiddles());
        _instance.StartCoroutine(LoadPlayerAndEnemies());
    }


    private static bool downloadIsInProgress = false;
    private static string currentPathToSaveTo = "";

    public static async Task DownloadFromFirebase(string pathToDownloadFrom, string pathToSaveTo)
    {
        downloadIsInProgress = true;
        currentPathToSaveTo = pathToSaveTo;
        FirebaseBridge.FirebaseStorage.DownloadFile(pathToDownloadFrom, _instance.gameObject.name, "OnDownloadCompleted", "OnDownloadFailed");
        while (downloadIsInProgress)
        {
            await Task.Delay(25);
        }
    }

    private void OnDownloadCompleted(string base64Result)
    {
        File.WriteAllBytes(currentPathToSaveTo, Convert.FromBase64String(base64Result));
        currentPathToSaveTo = "";
        downloadIsInProgress = false;
    }

    private void OnDownloadFailed(string error)
    {
        Debug.LogError(error);
        currentPathToSaveTo = "";
        downloadIsInProgress = false;
    }

    private static IEnumerator LoadPlayerAndEnemies()
    {
        yield return _instance.StartCoroutine(GetPlayerXmlFromDB("Player.xml"));
        yield return _instance.StartCoroutine(GetEnemyXmlFromDB("Goblin.xml"));

        OnEnemiesLoaded?.Invoke();
        EnemiesHaveBeenLoaded = true;
    }

    private static IEnumerator LoadRiddles()
    {
        Task task = DownloadFromFirebase("cs1332/fs29fh2d39823/Riddles/TAs.xml", Path.Combine(Files.RiddlesFolder, "TAs.xml").ToString());

        yield return new WaitWhile(() => !task.IsCompleted);

        OnRiddlesLoaded?.Invoke();
        RiddlesHaveBeenLoaded = true;
    }


    public static IEnumerator GetEnemyXmlFromDB(string enemyXmlFileName) {
        Task task = DownloadFromFirebase($"cs1332/fs29fh2d39823/Enemies/{enemyXmlFileName}", Path.Combine(Files.EnemiesFolder, enemyXmlFileName).ToString());

        yield return new WaitWhile(() => !task.IsCompleted);
    }

    public static IEnumerator GetPlayerXmlFromDB(string playerXmlFileName) {
        Task task = DownloadFromFirebase($"cs1332/fs29fh2d39823/{playerXmlFileName}", Path.Combine(Files.PlayerXml).ToString());

        yield return new WaitWhile(() => !task.IsCompleted);
    }
}
