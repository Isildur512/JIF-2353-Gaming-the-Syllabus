using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] private TMP_InputField emailField;
    [SerializeField] private TMP_InputField syllabusCodeField;

    [SerializeField] private string startSceneName;

    private void Awake()
    {
        InitializeSingleton();
    }

    public static void StartGame()
    {
        string email = _instance.emailField.text.Trim();
        string syllabusCode = _instance.syllabusCodeField.text.Trim();

        DatabaseManager.CurrentUserEmail = email;
        DatabaseManager.CurrentSyllabusCode = syllabusCode;

        DatabaseManager.VerifyAndWriteToDatabase(ContinueToGame, ShowDownloadFailure, false);
    }

    private static void ContinueToGame()
    {
        SceneManager.sceneLoaded += LoadFromDatabase;
        SceneManager.LoadScene(_instance.startSceneName);
    }

    private static void ShowDownloadFailure()
    {
        InvalidSyllabusCodeAlertManager.ShowAlert();
        Debug.LogError("Syllabus code or email was invalid or the download failed");
    }

    private static void LoadFromDatabase(Scene _, LoadSceneMode __)
    {
        Debug.LogError("a");
        DatabaseManager.LoadFromDatabase();
        SceneManager.sceneLoaded -= LoadFromDatabase;
    }
}
