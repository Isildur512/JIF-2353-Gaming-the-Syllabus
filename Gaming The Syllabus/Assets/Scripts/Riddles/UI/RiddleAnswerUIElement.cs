using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Represents a UI element used for selecting/entering an answer to a syllabus riddle.
/// This is an interface because we may support multiple types of answer selection in the future
/// (e.g. buttons, text boxes, etc)
/// </summary>
public interface IRiddleAnswerUIElement
{
    /// <summary>
    /// Used to get the current answer value from this element. For example, buttons would return the answer assigned to 
    /// them while a text field would return the user's current input.
    /// </summary>
    /// <returns></returns>
    public abstract RiddleAnswer GetAnswer();

    /// <summary>
    /// Do any necessary setup for the element here, such as setting its text component if applicable.
    /// </summary>
    /// <param name="associatedAnswer"></param>
    public abstract void Initialize(Riddle associatedRiddle, RiddleAnswer associatedAnswer); 

    public abstract Riddle AssociatedRiddle { get; }
}
