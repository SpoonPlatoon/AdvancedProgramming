using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---XML Saving namespaces---//
using System.Xml; //Includes XML classes
using System.Xml.Serialization; //Includes ability to 'serialize' classes
using System.IO; //Adds file io functionality

using System;
/*
[XmlRoot("Game Data")]
public class GameData
{
    //---All the game data to be saved to a file---//
    public Vector3 playerPosition;
    public void Save(string filePath)
    {
        //Create a tool for converting class into XMl
        var serializer = new XmlSerializer(typeof(GameData));
        //Generate a file stream and open file stream to path
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            //Save to file to path
            serializer.Serialize(stream, this);
        }
    }

    public void Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            //Create a tool for converting XML into class
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            //Generate a file stream with the mode 'Open' to read file
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                //Deserialize
                GameData data = serializer.Deserialize(stream) as GameData;
                //-----Load the game data values here-----//
                playerPosition = data.playerPosition;
            }
        }
    }
}
*/

[Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int score;
}
public class GameSave : MonoBehaviour
{
    #region Singleton
    public static GameSave Instance = null;
    private void Awake()
    {
        Instance = this;
        Load();
    }
    //private void OnDestroy()
    //{
    //    Instance = null;
    //}
    public static GameData GetData()
    {
        return Instance.data;
    }
    #endregion


    public GameData data = new GameData();
    public string fileName = "GameSave";

    public void Save()
    {
        // C:/Users/Manny/AppData/Local/CompanyName/ProductName/GameSave.xml
        string fullPath = Application.dataPath + "/Data/" + fileName + ".json";
        //data.Save(fullPath);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(fullPath, json);

        //Display where the file saved
        print("Saved to path: " + fullPath);
    }

    public void Load()
    {
        // C:/Users/Manny/AppData/Local/CompanyName/ProductName/GameSave.xml
        string fullPath = Application.dataPath + "/Data/" + fileName + ".json";
        //data.Load(fullPath);
        string json = File.ReadAllText(fullPath);
        data = JsonUtility.FromJson<GameData>(json);
        //Display where the file saved
        print("Loaded from path: " + fullPath);
    }

    public static bool Exists()
    {
        string fullPath = Application.dataPath + "/Data/" + Instance.fileName + ".json";
        return File.Exists(fullPath);
    }
}
