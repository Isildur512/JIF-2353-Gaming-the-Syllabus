using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Firebase;
using Firebase.Storage;
using Firebase.Extensions;
using System.Threading.Tasks;

public class DatabaseManager : Singleton<DatabaseManager>
{
    public static Action OnRiddlesLoaded;
    public static Action OnEnemiesLoaded;

    public static bool RiddlesHaveBeenLoaded { get; private set; }
    public static bool EnemiesHaveBeenLoaded { get; private set; }

    private static FirebaseStorage databaseStorage;
    private static StorageReference rootDirectory;

    private void Awake()
    {
        InitializeSingleton();

        databaseStorage = FirebaseStorage.DefaultInstance;
        rootDirectory = databaseStorage.GetReferenceFromUrl("gs://gamingthesyllabustest.appspot.com/");
    }

    private void Start()
    {
        // TODO: We need to load all the enemy and riddle files eventually. This is kind of a pain since there isn't just a "download folder" option.
        _instance.StartCoroutine(LoadRiddles());
        _instance.StartCoroutine(LoadPlayerAndEnemies());
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
        StorageReference riddlesFolder = rootDirectory.Child("cs1332/fs29fh2d39823/Riddles");

        Task task = riddlesFolder.Child("TAs.xml").GetFileAsync(System.IO.Path.Combine(Files.RiddlesFolder, "TAs.xml").ToString()).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("TAs.xml Downloaded");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

        yield return new WaitWhile(() => !task.IsCompleted);

        OnRiddlesLoaded?.Invoke();
        RiddlesHaveBeenLoaded = true;
    }


    public static IEnumerator GetEnemyXmlFromDB(string enemyXmlFileName) {
        StorageReference enemyFolder = rootDirectory.Child("cs1332/fs29fh2d39823/Enemies");
        Task task = enemyFolder.Child($"{enemyXmlFileName}").GetFileAsync(System.IO.Path.Combine(Files.EnemiesFolder, enemyXmlFileName).ToString()).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log($"{enemyXmlFileName} Downloaded");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

        yield return new WaitWhile(() => !task.IsCompleted);
    }

    public static IEnumerator GetPlayerXmlFromDB(string playerXmlFileName) {
        StorageReference playerFolder = rootDirectory.Child("cs1332/fs29fh2d39823/");
        Task task = playerFolder.Child($"{playerXmlFileName}").GetFileAsync(Files.PlayerXml).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log($"{playerXmlFileName} Downloaded");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

        yield return new WaitWhile(() => !task.IsCompleted);
    }
}
