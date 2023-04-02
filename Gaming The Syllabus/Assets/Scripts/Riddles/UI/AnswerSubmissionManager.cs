using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class that handles the user clicking the button to submit their answer to a riddle. 
/// <para>Setup: Attach to submit button and set onClick to invoke <see cref="SubmitAnswer"/></para>
/// </summary>
public class AnswerSubmissionManager : Singleton<AnswerSubmissionManager>
{

    public static bool isSubmitted;
    public static bool isCorrect;
    private List<IRiddleAnswerUIElement> selectedAnswers = new();

    private void Awake()
    {
        InitializeSingleton();
        isSubmitted = false;
        isCorrect = false;
    }

    public static void SetAnswerElementSelected(IRiddleAnswerUIElement element, bool isSelected, bool deselectAllOthers = false)
    {
        if (isSelected && !_instance.selectedAnswers.Contains(element))
        {
            if (deselectAllOthers)
            {
                _instance.selectedAnswers.Clear();
            }
            _instance.selectedAnswers.Add(element);
            
        }
        else if (_instance.selectedAnswers.Contains(element))
        {
            _instance.selectedAnswers.Remove(element);
        }
    }

    public void SubmitAnswer()
    {
        selectedAnswers.ForEach((answerElement) => SyllabusRiddleManager.AttemptAnswer(answerElement.AssociatedRiddle, answerElement.GetAnswer()));
        isSubmitted = true;
        SyllabusRiddleUIManager.SetUIActive(false);
    }
}
