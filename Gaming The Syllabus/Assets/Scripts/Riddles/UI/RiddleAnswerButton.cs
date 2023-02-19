using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A button the player clicks on to select. Supports select one (clicking a button deselects all others)
/// and select multiple.
/// </summary>
public class RiddleAnswerButton : MonoBehaviour, IRiddleAnswerUIElement
{
    public Riddle AssociatedRiddle => _associatedRiddle;
    private Riddle _associatedRiddle;
    public bool IsSelectedAnswer { get; set; }

    private RiddleAnswer associatedAnswer;

    [SerializeField] private TextMeshProUGUI text;

    public void OnClick()
    {
        AnswerSubmissionManager.SetAnswerElementSelected(this, true, deselectAllOthers: _associatedRiddle.TypeOfAnswer == AnswerType.SelectOne);
    }

    public RiddleAnswer GetAnswer()
    {
        return associatedAnswer;
    }

    public void Initialize(Riddle associatedRiddle, RiddleAnswer associatedAnswer)
    {
        _associatedRiddle = associatedRiddle;
        this.associatedAnswer = associatedAnswer;
        text.text = associatedAnswer.Answer;
    }
}
