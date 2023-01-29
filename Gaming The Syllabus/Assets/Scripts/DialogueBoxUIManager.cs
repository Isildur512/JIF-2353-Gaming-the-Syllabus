using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxUIManager : Singleton<DialogueBoxUIManager>
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI dialogueBoxText;
    private static LinkedList<string> dialogueStrings = new LinkedList<string>();

    private void Awake()
    {
        InitializeSingleton();
    }

    public static void addStringToDialogueBox(string strToAdd) {
        if (dialogueStrings.Count >= 5) {
            dialogueStrings.RemoveFirst();
        }
        dialogueStrings.AddLast(strToAdd);
        _instance.UpdateDialogueBox();
    }

    public void UpdateDialogueBox() {
        string newText = "";
        foreach (string str in dialogueStrings) {
            newText += str + "\n";
        }
        _instance.dialogueBoxText.text = newText;
    }

}
