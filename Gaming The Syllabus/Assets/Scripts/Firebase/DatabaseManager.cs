using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Firebase;
using Firebase.Storage;
using Firebase.Extensions;

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
        databaseStorage = FirebaseStorage.DefaultInstance;
        rootDirectory = databaseStorage.GetReferenceFromUrl("gs://gamingthesyllabustest.appspot.com/");
    }

    private void Start()
    {
        // TODO: We need to load all the enemy and riddle files eventually. This is kind of a pain since there isn't just a "download folder" option.
        LoadRiddles();
        LoadPlayerAndEnemies();
    }

    private static void LoadPlayerAndEnemies()
    {
        GetPlayerXmlFromDB("Player.xml");
        GetEnemyXmlFromDB("Goblin.xml");

        OnEnemiesLoaded?.Invoke();
        EnemiesHaveBeenLoaded = true;
    }

    private static void LoadRiddles()
    {
        StorageReference riddlesFolder = rootDirectory.Child("cs1332/fs29fh2d39823/Riddles");

        riddlesFolder.Child("TAs.xml").GetFileAsync(System.IO.Path.Combine(Files.RiddlesFolder, "TAs.xml").ToString()).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("File downloaded.");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

        OnRiddlesLoaded?.Invoke();
        RiddlesHaveBeenLoaded = true;
    }


    public static void GetEnemyXmlFromDB(string enemyXmlFileName) {
        StorageReference enemyFolder = rootDirectory.Child("cs1332/fs29fh2d39823/Enemies");
        enemyFolder.Child($"{enemyXmlFileName}").GetFileAsync(System.IO.Path.Combine(Files.EnemiesFolder, enemyXmlFileName).ToString()).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log($"{enemyXmlFileName} Downloaded");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }

    public static void GetPlayerXmlFromDB(string playerXmlFileName) {
        StorageReference playerFolder = rootDirectory.Child("cs1332/fs29fh2d39823/");
        playerFolder.Child($"{playerXmlFileName}").GetFileAsync(Files.PlayerXml).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log($"{playerXmlFileName} Downloaded");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }
}
