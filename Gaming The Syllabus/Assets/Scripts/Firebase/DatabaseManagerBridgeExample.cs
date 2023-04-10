using UnityEngine;
using System;

using FirebaseBridge;
using System.Xml;

public class DatabaseManagerBridge : MonoBehaviour
{
    public static Action OnRiddlesLoaded;
    public static Action OnEnemiesLoaded;

    public static bool RiddlesHaveBeenLoaded { get; private set; }
    public static bool EnemiesHaveBeenLoaded { get; private set; }

    private static string storageBasePath = "gs://gamingthesyllabustest.appspot.com/";



    private void Start()
    {
        // TODO: We need to load all the enemy and riddle files eventually. This is kind of a pain since there isn't just a "download folder" option.
        LoadFile("cs1332/fs29fh2d39823/Riddles/TAs.xml");
    }


    private static void LoadFile(string filePath)
    {
        // for implementation see Assets/Plugins/FirebaseWGL/firebasestorage.jslib
        FirebaseStorage.DownloadFile($"{storageBasePath}{filePath}", "RiddlesManager", "OnSuccessFileDownlaod", "OnError");
       
    }

    public static void OnSuccessFileDownlaod(string base64File)
    {
        byte[] bytes = Convert.FromBase64String(base64File);
        XmlDocument doc = new XmlDocument();
        string xml =System.Text.Encoding.UTF8.GetString(bytes);
        doc.LoadXml(xml);

        OnRiddlesLoaded?.Invoke();
        RiddlesHaveBeenLoaded = true;
    }

    public static void OnError(string error)
    {
        Debug.LogError(error);
    }

}
