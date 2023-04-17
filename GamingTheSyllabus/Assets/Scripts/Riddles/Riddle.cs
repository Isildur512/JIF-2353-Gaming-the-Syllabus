using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using UnityEngine;

public class Riddle : IXmlSerializable
{
    public string Name { get; private set; }
    public string Question { get; private set; }

    public AnswerType TypeOfAnswer { get; private set; }

    private List<RiddleAnswer> _answers = new List<RiddleAnswer>();
    public RiddleAnswer[] Answers
    {
        get => _answers.ToArray();
    }

    public RiddleAnswer[] CorrectAnswers
    {
        get => _answers.FindAll((answer) => answer.IsCorrect).ToArray();
    }

    /// <summary>
    /// The filepath to the XML file defining the ability the player receives when completing this riddle.
    /// </summary>
    public string RewardAbilityFilepath { get; private set; }

    public Riddle(string name, string question, params RiddleAnswer[] answers)
    {
        Name = name;
        Question = question;
        _answers = new List<RiddleAnswer>(answers);
    }

    public Riddle()
    {

    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        Name = reader.GetAttribute("name");
        Question = reader.GetAttribute("question");
        RewardAbilityFilepath = reader.GetAttribute("reward");

        reader.ReadToDescendant("answers");
        int numberOfExpectedAnswers = int.Parse(XmlUtilities.GetAttributeOrDefault(reader, "numberOfAnswers", "1"));

        reader.ReadToDescendant("answer");
        for (int i = 0; i < numberOfExpectedAnswers; i++)
        {
            string answerText = reader.GetAttribute("text");
            bool isCorrect = bool.Parse(XmlUtilities.GetAttributeOrDefault(reader, "isCorrect", "false"));

            _answers.Add(new RiddleAnswer(answerText, isCorrect));

            reader.ReadToNextSibling("answer");
        }
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new System.NotImplementedException();
    }
}

public struct RiddleAnswer
{
    public string Answer { get; private set; }
    public bool IsCorrect { get; private set; }

    public RiddleAnswer(string answer, bool isCorrect)
    {
        Answer = answer;
        IsCorrect = isCorrect;
    }

    public override bool Equals(object obj)
    {
        return obj is RiddleAnswer answer
            && answer.Answer == Answer
            && answer.IsCorrect == IsCorrect;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public enum AnswerType
{
    SelectOne,
    SelectMultiple,
    TextEntry
}
