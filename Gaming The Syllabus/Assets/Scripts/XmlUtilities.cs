using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlUtilities
{
    public static void Serialize(object item, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        StreamWriter writer = new StreamWriter($"{Application.dataPath}/{filePath}");
        serializer.Serialize(writer.BaseStream, item);
        writer.Close();
        Debug.Log($"Created XML file at {filePath}");
    }

    public static T Deserialize<T>(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StreamReader reader = new StreamReader($"{Application.dataPath}/{filePath}");
        T deserialized = (T)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        Debug.Log($"Deserialized XML file at {filePath}");
        return deserialized;
    }
}