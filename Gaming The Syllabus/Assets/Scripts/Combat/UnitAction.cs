using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;

public class UnitAction : IXmlSerializable
{
    /// <summary>
    /// Using this ability consumes a charge and at least one charge is required to use this ability.
    /// A value of -1 (default) indicates that this ability does not require or consume charges.
    /// </summary>
    public int CurrentCharges { get; private set; }

    public string actionName;
    private string actionDescription;
    public List<ActionEffect> effects;
    private CombatUnit[] currentTargets;

    public UnitAction(params ActionEffect[] effects)
    {
        this.effects = new List<ActionEffect>();
        foreach (ActionEffect effect in effects)
        {
            this.effects.Add(effect);
        }
    }

    public UnitAction()
    {

    }

    /// <summary>
    /// The behavior to occur when the action is performed. 
    /// </summary>
    public void Execute(System.Action onActionCompleted)
    {
        if (CurrentCharges == 0)
        {
            Debug.Log("Unit attempted to use action but did not have sufficient charges");
        } 
        else
        {
            CombatManager.StartCombatCoroutine(IExecute(onActionCompleted));
        }
        
    }

    private IEnumerator IExecute(System.Action onActionCompleted)
    {
        foreach (ActionEffect effect in effects)
        {
            if (effect.Target == TargetType.Current)
            {
                if (currentTargets == null)
                {
                    Debug.LogError("Effect in UnitAction tried to use current target but current target was null");
                    yield break;
                }
            }
            else
            {
                currentTargets = CombatManager.GetTargetsByType(effect.Target);
            }
            yield return new WaitForSeconds(effect.DelayInSecondsBeforeEffects);
            effect.Apply(currentTargets);
            yield return new WaitForSeconds(effect.DelayInSecondsAfterEffects);
        }
        onActionCompleted?.Invoke();
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        actionName = reader.GetAttribute("name");
        actionDescription = reader.GetAttribute("description");
        CurrentCharges = int.Parse(XmlUtilities.GetAttributeOrDefault(reader, "charges", "-1"));

        effects = new List<ActionEffect>();

        XmlReader effectsReader = reader.ReadSubtree();
        effectsReader.Read();

        int numberOfExpectedEffects = int.Parse(XmlUtilities.GetAttributeOrDefault(reader, "numberOfEffects", "1"));

        effectsReader.ReadToDescendant("effect");
        for (int i = 0; i < numberOfExpectedEffects; i++)
        {
            string typeString = effectsReader.GetAttribute("type");
            ActionEffects effectType = Enum.Parse<ActionEffects>(typeString);
            ActionEffect effect = (ActionEffect)Activator.CreateInstance(AllActionEffects.GetActionEffect(effectType));
            effect.ReadXml(effectsReader);

            effects.Add(effect);

            // If we don't do this we end up moving to the end of the file after reading the last effect...
            // This sucks but I can't think of a better way to do it at the moment
            if (i < numberOfExpectedEffects - 1)
            {
                effectsReader.ReadToNextSibling("effect");
            }
        }
    }

    public void WriteXml(XmlWriter writer)
    {
        Debug.Log("Writing action");
        writer.WriteAttributeString("name", actionName);
        writer.WriteAttributeString("description", actionDescription);
        writer.WriteAttributeString("numberOfEffects", effects.Count.ToString());
        writer.WriteAttributeString("charges", CurrentCharges.ToString());
        foreach (ActionEffect effect in effects)
        {
            writer.WriteStartElement("effect");
            effect.WriteXml(writer);
            writer.WriteEndElement();
        }
    }
}
