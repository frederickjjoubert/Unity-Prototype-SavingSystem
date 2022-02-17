using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JSONSaveSystem : MonoBehaviour
{

    public void Save(string saveFile, StateData stateData)
    {
        string json = JsonUtility.ToJson(stateData);
        WriteJSON(saveFile, json);
    }

    public StateData Load(string saveFile)
    {
        string json = ReadJSON(saveFile);
        StateData stateData = JsonUtility.FromJson<StateData>(json);
        return stateData;
    }

    public void Delete(string saveFile)
    {
        string saveFilePath = GetPath(saveFile);
        File.Delete(saveFilePath);
    }

    private void WriteJSON(string saveFile, string json)
    {
        string path = GetPath(saveFile);
        try
        {
            File.WriteAllText(path, json);
        }
        catch (Exception exception)
        {
            Debug.Log("Failed to write to " + path + " wtih exception: " + exception);
        }
    }

    private string ReadJSON(string saveFile)
    {
        string json = "";
        string path = GetPath(saveFile);
        if (File.Exists(path))
        {
            try
            {
                json = File.ReadAllText(path);
            }
            catch (Exception exception)
            {
                Debug.Log("Failed to read from " + path + " wtih exception: " + exception);
            }
        }
        return json;
    }

    private string GetPath(string saveFile)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }
        string path = Application.persistentDataPath + "/saves/" + saveFile + ".json";
        return path;
    }

}

// References
// 1. File ReadAllText / WriteAllText
// https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalltext?view=net-6.0
// https://docs.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-6.0
// 2. JSONSerialization
// https://docs.unity3d.com/Manual/JSONSerialization.html

