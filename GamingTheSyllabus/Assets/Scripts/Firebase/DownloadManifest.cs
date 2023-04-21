using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class DownloadManifest
{
    private List<string> _relativeDownloadPaths = new List<string>();
    public string[] RelativeDownloadPaths { get => _relativeDownloadPaths.ToArray(); }

    public DownloadManifest(string filePathToXmlManifest)
    {
        XmlDocument abilityDataXml = new XmlDocument();
        StreamReader reader = new StreamReader(filePathToXmlManifest);
        abilityDataXml.Load(reader);

        XmlNodeList items = abilityDataXml.GetElementsByTagName("item");
        for (int i = 0; i < items.Count; i++)
        {
            _relativeDownloadPaths.Add(items[i].InnerXml);
        }
    }
}
