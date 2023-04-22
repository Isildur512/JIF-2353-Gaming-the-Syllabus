using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Firebase;
using Firebase.Storage;
using Firebase.Extensions;
using System.Threading.Tasks;
using System.IO;
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

    /// <summary>
    /// The email the current user entered from the main menu. Used to save their completion of the game to the database.
    /// </summary>
    public static string CurrentUserEmail { get; set; }

    /// <summary>
    /// Used for loading all files from Firebase cloud storage.
    /// </summary>
    public static string CurrentSyllabusCode { get; set; }

    [Tooltip("Use this if needed for testing. Files are normally loaded by the MainMenuManager calling LoadFromDatabase().")]
    [SerializeField] private bool loadFilesOnStartup = false;
    [Tooltip("Will be used if loadFilesOnStartup is selected")]
    [SerializeField] private string defaultSyllabusCode = "cs1332/fs29fh2d39823";

    public enum Loadable
    {
        Everything,
        Player,
        Enemies,
        Combats,
        Riddles,
        Sprites
    }

    /// <returns>Whether the loadable has finished downloading or not</returns>
    public static bool ContentIsLoaded(Loadable loadableToCheck)
    {
        return loadingStatus[loadableToCheck];
    }

    private void Awake()
    {
        loadingStatus[Loadable.Everything] = false;
        loadingStatus[Loadable.Player] = false;
        loadingStatus[Loadable.Combats] = false;
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
        if (loadFilesOnStartup)
        {
            CurrentSyllabusCode = defaultSyllabusCode;
            LoadFromDatabase();
        }
    }

    public static void LoadFromDatabase()
    {
        _instance.StartCoroutine(LoadPlayer());
        _instance.StartCoroutine(IDownloadDirectory("Combats", Files.CombatsFolder, () => { SetLoadingStatus(Loadable.Combats, true); }));
        _instance.StartCoroutine(IDownloadDirectory("Enemies", Files.EnemiesFolder, () => { SetLoadingStatus(Loadable.Enemies, true); OnEnemiesLoaded?.Invoke(); }));
        _instance.StartCoroutine(IDownloadDirectory("Riddles", Files.RiddlesFolder, () => { SetLoadingStatus(Loadable.Riddles, true); OnRiddlesLoaded?.Invoke(); }));
        _instance.StartCoroutine(IDownloadDirectory("Sprites", Files.SpritesFolderAbsolute, () => { SetLoadingStatus(Loadable.Sprites, true); OnSpritesLoaded?.Invoke(); }));
    }

    private static IEnumerator IDownloadDirectory(string directoryToDownloadFrom, string directoryToSaveTo, Action onDownloadComplete = null)
    {
        Directory.CreateDirectory(directoryToSaveTo);

        StorageReference firebaseFolder = rootDirectory.Child($"{CurrentSyllabusCode}/{directoryToDownloadFrom}");
        Task task = DownloadFile(firebaseFolder.Child("manifest.xml"), Path.Combine(directoryToSaveTo, "manifest.xml").ToString());

        yield return new WaitWhile(() => !task.IsCompleted);

        DownloadManifest manifest = new DownloadManifest(Path.Combine(directoryToSaveTo, "manifest.xml").ToString());

        foreach (string path in manifest.RelativeDownloadPaths)
        {
            task = DownloadFile(firebaseFolder.Child(path), Path.Combine(directoryToSaveTo, path));
            yield return new WaitWhile(() => !task.IsCompleted);
        }

        onDownloadComplete?.Invoke();
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

        SetLoadingStatus(Loadable.Player, true);
    }

    private static void CheckIfAllLoadingIsCompleted()
    {
        // If none except for Loadable.Everything are incomplete
        if (loadingStatus.Values.ToList().FindAll((isCompleted) => !isCompleted).Count == 1)
        {
            loadingStatus[Loadable.Everything] = true;
            OnAllLoadingCompleted?.Invoke();
        }
    }

    private static void SetLoadingStatus(Loadable loadable, bool value)
    {
        loadingStatus[loadable] = true;
        CheckIfAllLoadingIsCompleted();
    }
}
