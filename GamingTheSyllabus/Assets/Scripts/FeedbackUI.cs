using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FeedbackUI : Singleton<FeedbackUI>
{
    [SerializeField] private TextMeshProUGUI feedbackText;

    private static LinkedList<string> feedbackStrings = new LinkedList<string>();

    void Awake()
    {
        InitializeSingleton(ifInstanceAlreadySetThenDestroy: this);
    }

    public static void AddStringTofeedback(string strToAdd) {
        if (feedbackStrings.Count >= 4) {
            feedbackStrings.RemoveFirst();
        }
        feedbackStrings.AddLast(strToAdd);
    }

    private void UpdateFeedback() {
        string newText = "";
        foreach (string str in feedbackStrings) {
            newText += " - " + str + "\n";
        }
        _instance.feedbackText.text = newText;
    }


    public static void NotifyUser(string feedbackMessage)
    {
        _instance.feedbackText.text = feedbackMessage;
        feedbackStrings.AddLast(feedbackMessage);
        _instance.StartCoroutine(_instance.DisplayFeedBackText());
    }


    IEnumerator DisplayFeedBackText()
    {
        _instance.UpdateFeedback();
        yield return new WaitForSeconds(5);
        feedbackStrings.Clear();
        _instance.feedbackText.text = "";
    }
}
