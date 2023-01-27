using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

/// <summary>
/// A unit participating in combat. Used by <see cref="CombatManager"/>.
/// </summary>
[XmlRoot("CombatUnit")]


public class CombatUnit : IXmlSerializable
{

    [XmlAttribute("name")]
    public string unitName;

    public int maximumHealth;
    public int currentHealth;

    public List<UnitAction> actions;

    public void PerformTurn(Action action) 
    {
        // Can only perform an action if it the requesters turn and the requester is not dead.
        if (CombatManager.turnQueue.Peek().Equals(this) && currentHealth > 0) {
            actions[action.Value].Execute();
            CombatManager.nextTurn();
        } else {
            Debug.Log("IT IS NOT YOUR TURN");
        }
    }

    public void ApplyDamage(int amount)
    {
        currentHealth = (currentHealth - amount < 0) ? 0 : currentHealth - amount; // If health would go below 0, then just set to 0.
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        unitName = reader.GetAttribute("name");
        maximumHealth = int.Parse(reader.GetAttribute("maximumHealth"));
        currentHealth = int.Parse(reader.GetAttribute("currentHealth"));

        Debug.Log($"Name: {unitName}, Max HP: {maximumHealth}, Current: {currentHealth}");

        // TODO: Make this work for any number of actions, not just 1
        actions = new List<UnitAction>();
        reader.ReadToDescendant("actions");

        UnitAction action = new UnitAction();
        action.ReadXml(reader);
        actions.Add(action);
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        writer.WriteAttributeString("name", unitName);
        writer.WriteAttributeString("maximumHealth", maximumHealth.ToString());
        writer.WriteAttributeString("currentHealth", currentHealth.ToString());

        writer.WriteStartElement("actions");

        foreach (UnitAction action in actions)
        {
            writer.WriteStartElement("action");
            action.WriteXml(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }
}
