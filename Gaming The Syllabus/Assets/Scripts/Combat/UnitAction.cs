using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;
using System.Reflection;

public class UnitAction : IXmlSerializable
{
    private string actionName;

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
    public void Execute()
    {
        foreach (ActionEffect effect in effects)
        {
            if (effect.Target == TargetType.Current)
            {
                if (currentTargets == null)
                {
                    Debug.LogError("Effect in UnitAction tried to use current target but current target was null");
                    return;
                }
            }
            else
            {
                currentTargets = CombatManager.GetTargetsByType(effect.Target);
            }

            effect.Apply(currentTargets);
        }
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        Debug.Log("Reading action");
        reader.ReadToDescendant("action");
        actionName = reader.GetAttribute("name");

        effects = new List<ActionEffect>();

        XmlReader effectsReader = reader.ReadSubtree();
        
        // TODO: Make this work for any number of effects, not just 1
        effectsReader.ReadToDescendant("effect");
        string typeString = effectsReader.GetAttribute("type");
        ActionEffects effectType = Enum.Parse<ActionEffects>(typeString);
        ActionEffect effect = (ActionEffect)Activator.CreateInstance(AllActionEffects.GetActionEffect(effectType));
        effect.ReadXml(effectsReader);

        effects.Add(effect);

        /*while (effectsReader.Read())
        {
            Debug.Log(effectsReader.AttributeCount);
            if (effectsReader.AttributeCount > 0)
            {
                string typeString = effectsReader.GetAttribute("type");
                ActionEffects effectType = Enum.Parse<ActionEffects>(typeString);
                ActionEffect effect = (ActionEffect)Activator.CreateInstance(AllActionEffects.GetActionEffect(effectType));
                effect.ReadXml(effectsReader);
            }

            effectsReader.MoveToElement();
        }*/
    }

    public void WriteXml(XmlWriter writer)
    {
        Debug.Log("Writing action");
        writer.WriteAttributeString("name", "actionName");
        foreach (ActionEffect effect in effects)
        {
            writer.WriteStartElement("effect");
            effect.WriteXml(writer);
            writer.WriteEndElement();
        }
    }
}
