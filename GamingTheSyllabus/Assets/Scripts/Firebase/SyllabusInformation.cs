using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public struct SyllabusInformation
{
    public string CourseName { get; private set; }
    public string Professor { get; private set; }
    public string Semester { get; private set; }

    public SyllabusInformation(string pathToXml)
    {
        XmlDocument xml = new XmlDocument();
        StreamReader reader = new StreamReader(pathToXml);
        xml.Load(reader);

        CourseName = xml["course-name"].InnerXml;
        Professor = xml["professor"].InnerXml;
        Semester = xml["semester"].InnerXml;
    }
}
