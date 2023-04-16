using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Files
{
    public static string RiddlesFolder { get => $"{CorrectDataPath}/XML/Riddles"; }
    public static string EnemiesFolder { get => $"{CorrectDataPath}/XML/Enemies"; }
    public static string PlayerXml { get => $"{CorrectDataPath}/XML/Player.xml"; }

    /// <summary>
    /// This returns the relative path from the Resources folder.
    /// </summary>
    public static string SpritesFolder { get => $"Sprites"; }

    /// <summary>
    /// If in editor, returns dataPath. Otherwise, returns persistentDataPath.
    /// This is necessary because we want to use the persistentDataPath in actual builds but this causes issues in the editor.
    /// </summary>
    public static string CorrectDataPath { get => Application.isEditor ? Application.dataPath : Application.persistentDataPath; }
}
