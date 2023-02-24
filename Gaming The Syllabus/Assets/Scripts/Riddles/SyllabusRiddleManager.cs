using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

#nullable enable
public class SyllabusRiddleManager : Singleton<SyllabusRiddleManager>
{
    private static List<Riddle>? riddles;
    public static HashSet<string>? solved_riddles;
    public static Riddle[]? Riddles { 
        get 
        {
            if (riddles == null)
            {
                LoadRiddlesFromXML("Assets/XML/Riddles");
            }
            return riddles?.ToArray();
        } 
    }

    private void Awake()
    {
        LoadRiddlesFromXML("Assets/XML/Riddles");
    }

    public static bool AttemptAnswer(Riddle riddle, RiddleAnswer answer)
    {
        // TODO: Make this give the player the ability reward
        // TODO: Make this punish the player for an incorrect answer
        // TODO: Potentially properly support "select all correct answers" questions depending on if the client wants them
        Debug.Log($"Attempted answer to riddle {riddle.Question} with answer {answer.Answer} and result {riddle.CorrectAnswers.Contains(answer)}");
        bool result = riddle.CorrectAnswers.Contains(answer);
        if (result && solved_riddles != null)
        {
            solved_riddles.Add(riddle.Question);
        }
        return result;
    }

    private static void LoadRiddlesFromXML(string filePathToRiddlesFolder)
    {
        riddles = new List<Riddle>();
        solved_riddles = new HashSet<string>();
        IEnumerable<string> riddlePaths = Directory.GetFiles(filePathToRiddlesFolder)
            .Where((path) => !path.Contains(".meta")); // Ignore meta files
        foreach (string riddlePath in riddlePaths)
        {
            // We end up with the Assets folder in the path twice since our XmlUtilities class uses the app data path which
            // is the path to the Assets folder, so we just remove it here.
            riddles.Add(XmlUtilities.Deserialize<Riddle>(riddlePath.Replace("Assets/", "")));
        }
    }

    public static bool AreAllRiddlesCompleted()
    {
        if (riddles != null && solved_riddles != null)
        {
            int num_of_riddles = riddles.Count;
            int num_of_solved_riddles = solved_riddles.Count;
            return num_of_riddles == num_of_solved_riddles;
        }
        return false;
    } 


}
