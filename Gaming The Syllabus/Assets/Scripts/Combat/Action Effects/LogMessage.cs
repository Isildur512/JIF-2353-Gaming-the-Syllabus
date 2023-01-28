using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

/// <summary>
/// Logs a message to the console for debugging.
/// </summary>
public class LogMessage : ActionEffect
{
    private string message;
    private LoggingLevel loggingLevel;

    public LogMessage(string message, LoggingLevel level)
    {
        this.message = message;
        this.loggingLevel = level;
    }

    public LogMessage()
    {

    }

    public override void Apply(params CombatUnit[] targets)
    {
        switch (loggingLevel)
        {
            case LoggingLevel.Log:
                Debug.Log(message);
                break;
            case LoggingLevel.Warning:
                Debug.LogWarning(message);
                break;
            case LoggingLevel.Error:
                Debug.LogError(message);
                break;
        }
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);
        writer.WriteAttributeString("message", message);
        writer.WriteAttributeString("level", loggingLevel.ToString());
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);
        message = reader.GetAttribute("message");
        if (!string.IsNullOrEmpty(reader.GetAttribute("loggingLevel")))
        {
            loggingLevel = System.Enum.Parse<LoggingLevel>(reader.GetAttribute("loggingLevel"));
        }
    }

    public enum LoggingLevel
    {
        Log,
        Warning,
        Error
    }
}
