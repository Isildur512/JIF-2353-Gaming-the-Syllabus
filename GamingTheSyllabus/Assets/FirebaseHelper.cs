using Firebase.Extensions;
using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseHelper : MonoBehaviour
{
    [Tooltip("This is where the helper will create/upload files from")]
    [SerializeField] private string pathToRootFolder;

    [SerializeField] private string syllabusCode;

    public void CreateDefaultDirectorySetup()
    {
        DeleteAllFilesInDirectoryRecursively(pathToRootFolder);
        CreateDirectoryAndManifest("Riddles");
        CreateDirectoryAndManifest("Enemies");
        CreateDirectoryAndManifest("Combats");
        CreateDirectoryAndManifest("Sprites");

        File.Create(Path.Combine(pathToRootFolder, "Player.xml"));
        File.Create(Path.Combine(pathToRootFolder, "syllabus-info.xml"));
    }

    public void UploadFilesToFirebase()
    {
        DatabaseManager.UploadDirectory(syllabusCode, $"{pathToRootFolder}/Riddles", "Riddles");
        DatabaseManager.UploadDirectory(syllabusCode, $"{pathToRootFolder}/Enemies", "Enemies");
        DatabaseManager.UploadDirectory(syllabusCode, $"{pathToRootFolder}/Combats", "Combats");
        DatabaseManager.UploadDirectory(syllabusCode, $"{pathToRootFolder}/Sprites", "Sprites");
    }


    private void CreateDirectoryAndManifest(string directoryName)
    {
        Directory.CreateDirectory(Path.Combine(pathToRootFolder, directoryName));
        File.Create(Path.Combine(pathToRootFolder, directoryName, "manifest.xml"));
    }

    private void DeleteAllFilesInDirectoryRecursively(string FolderName)
    {
        DirectoryInfo dir = new DirectoryInfo(FolderName);

        foreach (FileInfo fi in dir.GetFiles())
        {
            fi.Delete();
        }

        foreach (DirectoryInfo di in dir.GetDirectories())
        {
            DeleteAllFilesInDirectoryRecursively(di.FullName);
            di.Delete();
        }
    }
}
