using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Singleton class responsible for displaying riddles by instantiating and initializing answer elements at runtime.
/// Use <see cref="DisplayRiddle(Riddle)"/> to open the UI and display the passed in riddle. All logic required to
/// support the user answering the riddle is handled appropriately by this class and those related to it.
/// <para>Setup: Attach to Canvas and assign riddle uiPanel and question text</para>
/// </summary>
public class SyllabusRiddleUIManager : Singleton<SyllabusRiddleUIManager>
{
    [Header("UI Parameters")]
    [SerializeField] private int maximumNumberOfAnswers = 4;
    [SerializeField] private Vector2 firstAnswerPosition;
    [SerializeField] private float yDistanceBetweenAnswerElements;


    [Header("References")]
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI question;
    private List<IRiddleAnswerUIElement> riddleAnswerUIElements;

    [Header("Answer Element Prefabs")]
    [SerializeField] private GameObject buttonAnswerElementPrefab;

    public static bool UIIsActive { get => _instance.uiPanel.activeInHierarchy; }

    private void Awake()
    {
        InitializeSingleton();
    }

    public static void SetUIActive(bool isActive)
    {
        _instance.uiPanel.SetActive(isActive);
        Player.CanMove = !isActive;
    }

    public static void DisplayRiddle(Riddle riddle)
    {
        SetUIActive(true);
        _instance.question.text = riddle.Question;

        _instance.riddleAnswerUIElements = new List<IRiddleAnswerUIElement>();

        if (riddle.Answers.Length > _instance.maximumNumberOfAnswers)
        {
            Debug.LogWarning($"Riddle with question {riddle.Question} had {riddle.Answers.Length - _instance.riddleAnswerUIElements.Count} more answers than the riddle UI supports");
        }
        for (int i = 0; i < riddle.Answers.Length; i++)
        {
            CreateAnswerElement
            (
                _instance.buttonAnswerElementPrefab,
                _instance.uiPanel.transform,
                _instance.firstAnswerPosition + new Vector2(0, -(_instance.yDistanceBetweenAnswerElements * i)),
                riddle,
                riddle.Answers[i]
            );
        }
    }

    public static void DisplayRiddle(int indexOfRiddle)
    {
        DisplayRiddle(SyllabusRiddleManager.Riddles[indexOfRiddle]);
    }

    private static void CreateAnswerElement(GameObject prefab, Transform containingPanel, Vector2 position, Riddle riddle, RiddleAnswer answer)
    {
        GameObject answerElement = Instantiate(prefab, _instance.transform);
        answerElement.GetComponent<IRiddleAnswerUIElement>().Initialize(riddle, answer);

        RectTransform answerElementTransform = answerElement.GetComponent<RectTransform>();
        answerElementTransform.anchoredPosition = position;

        answerElement.transform.SetParent(containingPanel);
    }
}
