using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;

using Firebase;
using Firebase.Storage;
using Firebase.Extensions;
using Firebase.Database;
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

    private static FirebaseStorage firebaseStorage;
    private static StorageReference storageRootDirectory;
    private static FirebaseDatabase firebaseDatabase;
    private static DatabaseReference databaseRootDirectory;

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
        if (_instance == null)
        {
            InitializeSingleton();
            DontDestroyOnLoad(gameObject);

            loadingStatus[Loadable.Everything] = false;
            loadingStatus[Loadable.Player] = false;
            loadingStatus[Loadable.Combats] = false;
            loadingStatus[Loadable.Enemies] = false;
            loadingStatus[Loadable.Riddles] = false;
            loadingStatus[Loadable.Sprites] = false;

            AppOptions options = new AppOptions();
            options.ApiKey = "AIzaSyA-vRiqVfQKPj18OfZOMol-BnsARDUqSk4";
            options.AppId = "1:575623898016:ios:51daf5d9dbebffd08afac5";
            options.MessageSenderId = "575623898016-e15ti1nv1paa2o67395124hqqlkmp4u3.apps.googleusercontent.com";
            options.ProjectId = "gamingthesyllabustest";
            options.StorageBucket = "gamingthesyllabustest.appspot.com";
            options.DatabaseUrl = new Uri("https://gamingthesyllabustest-default-rtdb.firebaseio.com");

            var app = FirebaseApp.Create(options);

            firebaseStorage = FirebaseStorage.DefaultInstance;
            storageRootDirectory = firebaseStorage.GetReferenceFromUrl("gs://gamingthesyllabustest.appspot.com/");

            firebaseDatabase = FirebaseDatabase.DefaultInstance;
            databaseRootDirectory = firebaseDatabase.RootReference;
        } else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        if (loadFilesOnStartup)
        {
            Debug.Log("Loading files on startup (this should not happen in release builds!)");
            CurrentSyllabusCode = defaultSyllabusCode;
            LoadFromDatabase();
        }

    }

    public static void AttemptToDownloadSyllabusInformation(Action onDownloadSucceeded, Action onDownloadFailed)
    {
        storageRootDirectory.Child($"{CurrentSyllabusCode}/syllabus-info.xml").GetFileAsync(Files.SyllabusInformationXml).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                onDownloadSucceeded?.Invoke();
            }
            else
            {
                onDownloadFailed?.Invoke();
            }
        });
    }


    public static void LoadFromDatabase()
    {
        if (loadingStatus[Loadable.Everything])
        {
            return;
        }
        _instance.StartCoroutine(LoadPlayer());
        _instance.StartCoroutine(IDownloadDirectory("Combats", Files.CombatsFolder, () => { SetLoadingStatus(Loadable.Combats, true); }));
        _instance.StartCoroutine(IDownloadDirectory("Enemies", Files.EnemiesFolder, () => { SetLoadingStatus(Loadable.Enemies, true); OnEnemiesLoaded?.Invoke(); }));
        _instance.StartCoroutine(IDownloadDirectory("Riddles", Files.RiddlesFolder, () => { SetLoadingStatus(Loadable.Riddles, true); OnRiddlesLoaded?.Invoke(); }));
        _instance.StartCoroutine(IDownloadDirectory("Sprites", Files.SpritesFolderAbsolute, () => {SetLoadingStatus(Loadable.Sprites, true); OnSpritesLoaded?.Invoke(); }));
    }

    private static IEnumerator IDownloadDirectory(string directoryToDownloadFrom, string directoryToSaveTo, Action onDownloadComplete = null)
    {
        Directory.CreateDirectory(directoryToSaveTo);

        StorageReference firebaseFolder = storageRootDirectory.Child($"{CurrentSyllabusCode}/{directoryToDownloadFrom}");
        Task task = DownloadFile(firebaseFolder.Child("manifest.xml"), Path.Combine(directoryToSaveTo, "manifest.xml").ToString());

        yield return new WaitWhile(() => !task.IsCompleted);

        DownloadManifest manifest = new DownloadManifest(Path.Combine(directoryToSaveTo, "manifest.xml").ToString());

        foreach (string path in manifest.RelativeDownloadPaths)
        {
            task = DownloadFile(firebaseFolder.Child(path), Path.Combine(directoryToSaveTo, path));
            yield return new WaitWhile(() => !task.IsCompleted);
        }

        yield return new WaitForEndOfFrame();

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
        Task task = DownloadFile(storageRootDirectory.Child($"{CurrentSyllabusCode}/Player.xml"), Files.PlayerXml);

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

    private static void WriteToDatabase(Action onDownloadSucceeded, Action onDownloadFailed, bool status) {
        DatabaseReference firebaseFolder = databaseRootDirectory.Child(CurrentSyllabusCode);
        string username = CurrentUserEmail.Substring(0, CurrentUserEmail.IndexOf("@"));
        firebaseFolder.Child(username).SetValueAsync(status).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log($"Data saved to ${CurrentSyllabusCode} from user: {username}");
                AttemptToDownloadSyllabusInformation(onDownloadSucceeded, onDownloadFailed);
            }
            else
            {
                Debug.Log(task.Exception);
                onDownloadFailed?.Invoke();
            }
        });
    }

    public static void VerifyAndWriteToDatabase(Action onDownloadSucceeded, Action onDownloadFailed, bool status) {
        DatabaseReference firebaseFolder = databaseRootDirectory.Child(CurrentSyllabusCode);
        // check if this folder path actually exists in the database
        firebaseFolder.GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception);

            } else if (task.IsCompleted)
            {
                DataSnapshot snapShot = task.Result;
                if (snapShot.Value != null) {
                    if (!IsValidEmail(CurrentUserEmail)) {
                        onDownloadFailed?.Invoke();
                    } else {
                        WriteToDatabase(onDownloadSucceeded, onDownloadFailed, status);
                    }
                } else {
                    onDownloadFailed?.Invoke();
                }
            }
        });
    }

    private static bool IsValidEmail(string email) {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        Regex validEmailRegex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

        return validEmailRegex.IsMatch(email);
    }
}
