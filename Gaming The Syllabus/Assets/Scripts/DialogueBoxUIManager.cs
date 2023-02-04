using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Singleton UI manager that specifically manages the Dialogue Box within the UI.
/// </summary>
public class DialogueBoxUIManager : Singleton<DialogueBoxUIManager>
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI dialogueBoxText;
    private static LinkedList<string> dialogueStrings = new LinkedList<string>();

    private void Awake()
    {
        InitializeSingleton();
    }

    /// <summary>
    /// Adds a string (strToAdd) to the dialogue box within the UI.
    /// </summary>
    /// <param name="strToAdd">The string to be added to the dialogue box</param>
    public static void addStringToDialogueBox(string strToAdd) {
        if (dialogueStrings.Count >= 5) {
            dialogueStrings.RemoveFirst();
        }
        dialogueStrings.AddLast(strToAdd);
        _instance.UpdateDialogueBox();
    }

    /// <summary>
    /// Loops through the LinkedList of strings and concatenates them together and then updates
    /// the actual dialogue box text mesh pro text.
    /// </summary>
    private void UpdateDialogueBox() {
        string newText = "";
        foreach (string str in dialogueStrings) {
            newText += str + "\n";
        }
        _instance.dialogueBoxText.text = newText;
    }

}