using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

#nullable enable
public class SyllabusRiddleManager : Singleton<SyllabusRiddleManager>
{
    private static List<Riddle>? riddles;
    private static HashSet<Riddle> solvedRiddles = new HashSet<Riddle>();
    public static Riddle[]? Riddles { get => riddles?.ToArray(); }

    public static HashSet<Scene> roomsCompleted = new HashSet<Scene>();

    private void Awake()
    {
        InitializeSingleton(ifInstanceAlreadySetThenDestroy: this);
        DontDestroyOnLoad(gameObject);

        DatabaseManager.OnRiddlesLoaded += () => { LoadRiddlesFromXML(Files.RiddlesFolder); };
    }

    public static bool AttemptAnswer(Riddle riddle, RiddleAnswer answer)
    {
        // TODO: Make this give the player the ability reward
        // TODO: Make this punish the player for an incorrect answer
        // TODO: Potentially properly support "select all correct answers" questions depending on if the client wants them
        Debug.Log($"Attempted answer to riddle {riddle.Question} with answer {answer.Answer} and result {riddle.CorrectAnswers.Contains(answer)}");
        bool result = riddle.CorrectAnswers.Contains(answer);
        if (result && solvedRiddles != null)
        {
            solvedRiddles.Add(riddle);
            roomsCompleted.Add(SceneManager.GetActiveScene());
        }
        AnswerSubmissionManager.isCorrect = result;
        return result;
    }

    public static void LoadRiddlesFromXML(string filePathToRiddlesFolder)
    {
        Debug.Log("Riddles Loaded from XML");
        riddles = new List<Riddle>();
        IEnumerable<string> riddlePaths = Directory.GetFiles(filePathToRiddlesFolder)
            .Where((path) => !path.Contains(".meta") && !path.Contains("manifest")); // Ignore meta and manifest files
        foreach (string riddlePath in riddlePaths)
        {
            Riddle riddle = XmlUtilities.Deserialize<Riddle>(riddlePath);
            riddles.Add(riddle);
        }
    }

    public static bool AreAllRiddlesCompleted()
    {
        if (riddles != null && solvedRiddles != null)
        {
            int numOfRiddles = riddles.Count;
            int numOfSolvedRiddles = solvedRiddles.Count;
            return numOfRiddles == numOfSolvedRiddles;
        }
        return false;
    } 
}
