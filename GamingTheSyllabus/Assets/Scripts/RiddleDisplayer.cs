using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleDisplayer : MonoBehaviour
{
    [SerializeField] private int indexOfRiddleToDisplay;

    public void DisplayRiddle()
    {
        SyllabusRiddleUIManager.DisplayRiddle(indexOfRiddleToDisplay);
    }
}
