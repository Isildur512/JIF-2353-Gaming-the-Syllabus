using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Storage;
using Firebase.Extensions;

public class DatabaseManager : MonoBehaviour
{
    FirebaseStorage databaseStorage;
    StorageReference rootDirectory;

    private void Start()
    {
        databaseStorage = FirebaseStorage.DefaultInstance;
        rootDirectory = databaseStorage.GetReferenceFromUrl("gs://gamingthesyllabustest.appspot.com/");

        StorageReference riddlesFolder = rootDirectory.Child("cs1332/fs29fh2d39823/Riddles");

        riddlesFolder.Child("TAs.xml").GetFileAsync(Application.streamingAssetsPath + "/XML/TAs.xml").ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("File downloaded.");
                SyllabusRiddleManager.LoadRiddlesFromXML(Application.streamingAssetsPath + "/XML/Test");
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

        /*riddlesFolder.Child("TAs.xml").GetFileAsync(Application.streamingAssetsPath + "/XML/Test").ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("File downloaded.");
                DebugTextManager.Log(Application.streamingAssetsPath + "/XML");
                Debug.Log(Application.streamingAssetsPath + "/XML");
                SyllabusRiddleManager.LoadRiddlesFromXML(Application.streamingAssetsPath + "/XML");
            } else
            {
                Debug.Log(task.Exception);
            }
        });*/

    }
}
