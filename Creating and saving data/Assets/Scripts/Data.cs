using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

public class Data : MonoBehaviour
{
    string dataPath;
    string xmlFilePath;
    string jsonFilePath;
    List<GroupMember> Group = new List<GroupMember> {
        new GroupMember("Christian", "20/01/2004", "Blue"),
        new GroupMember("Vahid", "11/05/2004", "Black"),
        new GroupMember("Asser", "12/01/1985", "Red"),
        new GroupMember("Meron", "23/10/2003", "Blue/Purple"),
    };
    Classroom MED2 = new Classroom();

    // Sets the values of path variables
    private void Awake()
    {
        dataPath = "Assets/Group_Data/";
        xmlFilePath = dataPath + "Group_Member_Info.xml";
        jsonFilePath = dataPath + "Group_Member_InfoJSON.json";
    }
    // Calls methods
    private void Start()
    {
        CreateDirectory();
        CreateFile(xmlFilePath);
        SerializeXml(xmlFilePath);
        DeserializeXml(xmlFilePath);
        SerializeJson(jsonFilePath);
    }
    /// <summary>
    /// Creates a directory
    /// </summary>
    void CreateDirectory()
    {
        if (Directory.Exists(dataPath))
        {
            Debug.Log("Directory exists");
        }
        else
        {
            Directory.CreateDirectory(dataPath);
            Debug.Log("Directory created");
        }
    }
    /// <summary>
    /// Creates a file
    /// </summary>
    /// <param name="path"></param>
    void CreateFile(string path)
    {
        if (File.Exists(path))
        {
            Debug.Log("File exists");
        }
        else
        {
            File.WriteAllText(path, "<GROUP MEMBERS>\n");
            Debug.Log("File created");
        }
    }
    /// <summary>
    /// Serializes a file in xml
    /// </summary>
    /// <param name="path"></param>
    void SerializeXml(string path) {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<GroupMember>));

        using (FileStream fStream = File.Create(path))
        {
            xmlSerializer.Serialize(fStream, Group);
        }
    }
    /// <summary>
    /// Deserializes a file from xml to MED2.Group10
    /// </summary>
    /// <param name="path"></param>
    void DeserializeXml(string path)
    {
        if (File.Exists(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<GroupMember>));
            using (FileStream fStream = File.OpenRead(path))
            {
                MED2.Group10 = (List<GroupMember>)xmlSerializer.Deserialize(fStream);
            }
        }
    }
    /// <summary>
    /// Serializes a file in Json
    /// </summary>
    /// <param name="path"></param>
    void SerializeJson(string path)
    {
        string jsonString = JsonUtility.ToJson(MED2, true);
        using (StreamWriter sWriter = File.CreateText(path))
        {
            sWriter.WriteLine(jsonString);
        }
    }
}

/// <summary>
/// Class of groupmembers with variables: name, dateOfBirth, favouriteColour
/// </summary>
[Serializable]
public class GroupMember
{
    public string name;
    public string dateOfBirth;
    public string favouriteColour;

    public GroupMember(string name, string dateOfBirth, string favColour)
    {
        this.name = name;
        this.dateOfBirth = dateOfBirth;
        favouriteColour = favColour;
    }
    public GroupMember() { }
}

/// <summary>
/// Class with a list of groupmembers: Group10
/// </summary>
public class Classroom
{
    public List<GroupMember> Group10;
}