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
using System.Linq;

public class DatabaseManager : Singleton<DatabaseManager>
{
    public static Action OnRiddlesLoaded;
    public static Action OnEnemiesLoaded;
    public static Action OnSpritesLoaded;
    public static Action OnAllLoadingCompleted;

    private static Dictionary<Loadable, bool> loadingStatus = new Dictionary<Loadable, bool>();

    private static FirebaseStorage databaseStorage;
    private static StorageReference rootDirectory;

    public enum Loadable
    {
        Player,
        Enemies,
        Riddles,
        Sprites
    }

    /// <returns>Whether the loadable has finished downloading or not</returns>
    public static bool GetLoadingStatus(Loadable loadableToCheck)
    {
        return loadingStatus[loadableToCheck];
    }

    private void Awake()
    {
        loadingStatus[Loadable.Player] = false;
        loadingStatus[Loadable.Enemies] = false;
        loadingStatus[Loadable.Riddles] = false;
        loadingStatus[Loadable.Sprites] = false;

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
        _instance.StartCoroutine(LoadRiddles());
        _instance.StartCoroutine(LoadPlayer());
        _instance.StartCoroutine(LoadEnemies());
        _instance.StartCoroutine(LoadSprites());
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

        SetLoadingStatus(Loadable.Enemies, true);
        OnEnemiesLoaded?.Invoke();
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

        SetLoadingStatus(Loadable.Player, true);
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

        SetLoadingStatus(Loadable.Riddles, true);
        OnRiddlesLoaded?.Invoke();
    }

    private static IEnumerator LoadSprites()
    {
        Directory.CreateDirectory(Files.SpritesFolderAbsolute);

        StorageReference spritesFolder = rootDirectory.Child("cs1332/fs29fh2d39823/Sprites");
        Task task = DownloadFile(spritesFolder.Child("manifest.xml"), Path.Combine(Files.SpritesFolderAbsolute, "manifest.xml").ToString());

        yield return new WaitWhile(() => !task.IsCompleted);

        DownloadManifest manifest = new DownloadManifest(Path.Combine(Files.SpritesFolderAbsolute, "manifest.xml").ToString());

        foreach (string path in manifest.RelativeDownloadPaths)
        {
            task = DownloadFile(spritesFolder.Child(path), Path.Combine(Files.SpritesFolderAbsolute, path));
            yield return new WaitWhile(() => !task.IsCompleted);
        }

        SetLoadingStatus(Loadable.Sprites, true);
        OnSpritesLoaded?.Invoke();
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

    private static void CheckIfAllLoadingIsCompleted()
    {
        // If none are incomplete
        if (loadingStatus.Values.ToList().FindAll((isCompleted) => !isCompleted).Count == 0)
        {
            OnAllLoadingCompleted?.Invoke();
        }
    }

    private static void SetLoadingStatus(Loadable loadable, bool value)
    {
        loadingStatus[loadable] = true;
        CheckIfAllLoadingIsCompleted();
    }
}
