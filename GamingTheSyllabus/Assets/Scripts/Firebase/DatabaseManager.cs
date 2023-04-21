using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Firebase;
using Firebase.Storage;
using Firebase.Extensions;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

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

        AppOptions options = new AppOptions();
        options.ApiKey = "AIzaSyA-vRiqVfQKPj18OfZOMol-BnsARDUqSk4";
        options.AppId = "1:575623898016:ios:51daf5d9dbebffd08afac5";
        options.MessageSenderId = "575623898016-e15ti1nv1paa2o67395124hqqlkmp4u3.apps.googleusercontent.com";
        options.ProjectId = "gamingthesyllabustest";
        options.StorageBucket = "gamingthesyllabustest.appspot.com";

        var app = FirebaseApp.Create(options);

        databaseStorage = FirebaseStorage.DefaultInstance;
        rootDirectory = databaseStorage.GetReferenceFromUrl("gs://gamingthesyllabustest.appspot.com/");
    }

    private void Start()
    {
        // TODO: We need to load all the enemy and riddle files eventually. This is kind of a pain since there isn't just a "download folder" option.
        _instance.StartCoroutine(LoadRiddles());
        _instance.StartCoroutine(LoadPlayer());
        _instance.StartCoroutine(LoadEnemies());
    }

    private static IEnumerator LoadEnemies()
    {
        Directory.CreateDirectory(Files.EnemiesFolder);

        StorageReference enemiesFolder = rootDirectory.Child("cs1332/fs29fh2d39823/Enemies");
        Task task = DownloadFile(enemiesFolder.Child("manifest.xml"), Path.Combine(Files.EnemiesFolder, "manifest.xml").ToString());

        yield return new WaitWhile(() => !task.IsCompleted);

        DownloadManifest manifest = new DownloadManifest(Path.Combine(Files.EnemiesFolder, "manifest.xml").ToString());

        foreach (string path in manifest.RelativeDownloadPaths)
        {
            task = DownloadFile(enemiesFolder.Child(path), Path.Combine(Files.EnemiesFolder, path));
            yield return new WaitWhile(() => !task.IsCompleted);
        }

        yield return new WaitWhile(() => !task.IsCompleted);

        OnEnemiesLoaded?.Invoke();
        EnemiesHaveBeenLoaded = true;
    }

    private static IEnumerator LoadRiddles()
    {
        Directory.CreateDirectory(Files.RiddlesFolder);

        StorageReference riddlesFolder = rootDirectory.Child("cs1332/fs29fh2d39823/Riddles");
        Task task = DownloadFile(riddlesFolder.Child("manifest.xml"), Path.Combine(Files.RiddlesFolder, "manifest.xml").ToString());

        yield return new WaitWhile(() => !task.IsCompleted);

        DownloadManifest manifest = new DownloadManifest(Path.Combine(Files.RiddlesFolder, "manifest.xml").ToString());

        foreach (string path in manifest.RelativeDownloadPaths)
        {
            task = DownloadFile(riddlesFolder.Child(path), Path.Combine(Files.RiddlesFolder, path));
            yield return new WaitWhile(() => !task.IsCompleted);
        }

        OnRiddlesLoaded?.Invoke();
        RiddlesHaveBeenLoaded = true;
    }

    private static Task DownloadFile(StorageReference storageReference, string filePathToSaveAt) 
    {
        return storageReference.GetFileAsync(filePathToSaveAt).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log($"Download to {filePathToSaveAt} completed");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }

    public static IEnumerator LoadPlayer() {
        StorageReference playerFolder = rootDirectory.Child("cs1332/fs29fh2d39823/");
        Task task = playerFolder.Child($"Player.xml").GetFileAsync(Files.PlayerXml).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log($"Player.xml Downloaded");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

        yield return new WaitWhile(() => !task.IsCompleted);
    }
}
