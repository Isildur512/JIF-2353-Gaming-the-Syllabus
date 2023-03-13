using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class XmlUtilities
{
    public static void Serialize(object item, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        StreamWriter writer = new StreamWriter(Path.Combine(Application.dataPath, filePath));
        serializer.Serialize(writer.BaseStream, item);
        writer.Close();
        Debug.Log($"Created XML file at {filePath}");
    }

    public static T Deserialize<T>(string filePath)
    {

        XmlSerializer serializer = new XmlSerializer(typeof(T));
        // Worth remembering that Path.Combine will just return the 2nd filePath if it is absolute (starts with a /)
        StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, filePath));
        T deserialized = (T)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        Debug.Log($"Deserialized XML file at {filePath}");
        return deserialized;
    }

    public static T DeserializeFromAbsolutePath<T>(string absoluteFilePath)
    {
        Debug.Log(absoluteFilePath);

        XmlSerializer serializer = new XmlSerializer(typeof(T));
        // Worth remembering that Path.Combine will just return the 2nd filePath if it is absolute (starts with a /)
        StreamReader reader = new StreamReader(absoluteFilePath);
        T deserialized = (T)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        Debug.Log($"Deserialized XML file at {absoluteFilePath}");
        return deserialized;
    }

    /// <summary>
    /// Returns the default value if the attribute is not found or the value of the attribute is an empty string.
    /// Otherwise, returns the value of the attribute.
    /// </summary>
    public static string GetAttributeOrDefault(XmlReader reader, string attribute, string defaultValue)
    {
        string attributeValue = reader.GetAttribute(attribute);
        if (!string.IsNullOrEmpty(attributeValue))
        {
            return attributeValue;
        } else
        {
            return defaultValue;
        }
    }
}