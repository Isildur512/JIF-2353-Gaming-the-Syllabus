using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;
using System.IO;

/// <summary>
/// A unit participating in combat. Used by <see cref="CombatManager"/>.
/// </summary>
[XmlRoot("CombatUnit")]
public class CombatUnit : IXmlSerializable
{
    public string UnitName { get; private set; }
    public int MaximumHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public string dialogueColor { get; private set; }

    public List<UnitAction> actions { get; private set; }

    public List<PlayerAbility> abilities { get; private set; }

    public bool IsAlive { get; private set; } = true;

    public Sprite Sprite { get; private set; }

    public void PerformAction(int actionIndex)
    {
        if (IsAlive)
        {
            if (actionIndex < actions.Count)
            {
                actions[actionIndex].Execute(onActionCompleted: CombatManager.NextTurn);
            }
            else
            {
                Debug.LogError("Action index was outside of list of actions");
                CombatManager.NextTurn();
            }
        }
    }

    public async void PerformRandomAction()
    {
        if (IsAlive)
        {
            int randomIndex = UnityEngine.Random.Range(0, actions.Count);
            if (UnitName == "FinalBoss") {
                await DamageTargetByRiddle.HandleRiddleResult();
            }
            actions[randomIndex].Execute(onActionCompleted: CombatManager.NextTurn);
        }
    }

    public void ApplyDamage(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaximumHealth);
        IsAlive = CurrentHealth > 0;
        CombatUIManager.UpdateUnitHealthbar(this, CurrentHealth);
        CombatUIManager.UpdateHealthbarText(this, -amount);
    }

    public void HealUnit(int amount) {
        if (IsAlive) {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaximumHealth);
            Debug.Log($"New Health: {CurrentHealth}");
            CombatUIManager.UpdateUnitHealthbar(this, CurrentHealth);
            CombatUIManager.UpdateHealthbarText(this, amount);
        }
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        UnitName = reader.GetAttribute("name");
        MaximumHealth = int.Parse(reader.GetAttribute("maximumHealth"));
        CurrentHealth = int.Parse(reader.GetAttribute("currentHealth"));
        dialogueColor = reader.GetAttribute("dialogueColor");
        if (reader.GetAttribute("sprite") != null)
        {
            // Don't forget we don't use file extensions when loading from the resources folder
            Sprite = Resources.Load<Sprite>(Path.Combine(Files.SpritesFolder, reader.GetAttribute("sprite")));
        }

        actions = new List<UnitAction>();
        abilities = new List<PlayerAbility>();

        reader.ReadToDescendant("actions");

        int numberOfExpectedActions = int.Parse(XmlUtilities.GetAttributeOrDefault(reader, "numberOfActions", "1"));

        for (int i = 0; i < numberOfExpectedActions; i++)
        {
            UnitAction action = new UnitAction();
            action.ReadXml(reader);
            actions.Add(action);

            // This looks bad and probably is but we need to read to the closing tag and then read to the next opening tag
            reader.ReadToNextSibling("action");
            reader.ReadToNextSibling("action");
        }

        
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        writer.WriteAttributeString("name", UnitName);
        writer.WriteAttributeString("maximumHealth", MaximumHealth.ToString());
        writer.WriteAttributeString("currentHealth", CurrentHealth.ToString());

        writer.WriteStartElement("actions");
        writer.WriteAttributeString("numberOfActions", actions.Count.ToString());

        foreach (UnitAction action in actions)
        {
            writer.WriteStartElement("action");
            action.WriteXml(writer);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }
}
