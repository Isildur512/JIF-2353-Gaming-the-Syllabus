using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Files
{
    public static string RiddlesFolder { get => $"{CorrectDataPath}/XML/Riddles"; }
    public static string EnemiesFolder { get => $"{CorrectDataPath}/XML/Enemies"; }
    public static string CombatsFolder { get => $"{CorrectDataPath}/XML/Combats"; }
    public static string PlayerXml { get => $"{CorrectDataPath}/XML/Player.xml"; }
    public static string SyllabusInformationXml { get => $"{CorrectDataPath}/XML/syllabus-info.xml"; }

    /// <summary>
    /// This returns the relative path from the Resources folder.
    /// </summary>
    public static string SpritesFolderRelative { get => $"Sprites"; }

    public static string SpritesFolderAbsolute { get => $"{CorrectDataPath}/Resources/Sprites"; }

    /// <summary>
    /// If in editor, returns dataPath. Otherwise, returns persistentDataPath.
    /// This is necessary because we want to use the persistentDataPath in actual builds but this causes issues in the editor.
    /// </summary>
    public static string CorrectDataPath { get => Application.isEditor ? Application.dataPath : Application.persistentDataPath; }
}