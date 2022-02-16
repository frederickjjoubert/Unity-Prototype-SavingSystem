using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingSystem : MonoBehaviour
{

    BinaryFormatter binaryFormatter = new BinaryFormatter();

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        SurrogateSelector surrogateSelector = new SurrogateSelector();

        // Surrogates
        Vector3SerializationSurrogate vector3SerializationSurrogate = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quaternionSerializationSurrogate = new QuaternionSerializationSurrogate();

        // Add Surrogates
        surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.File), vector3SerializationSurrogate);
        surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.File), quaternionSerializationSurrogate);

        return binaryFormatter;
    }

    public void Save(string saveFile)
    {
        Dictionary<string, object> state = LoadFile(saveFile);
        CaptureState(state);
        SaveFile(saveFile, state);
    }

    public void Load(string saveFile)
    {
        Dictionary<string, object> state = LoadFile(saveFile);
        if (state.Count == 0)
        {
            // state = defaultState ??
        }
        RestoreState(state);
    }

    public void Delete(string saveFile)
    {
        string saveFilePath = GetPathFromSaveFile(saveFile);
        File.Delete(saveFilePath);
    }

    public IEnumerator LoadLastScene(string saveFile)
    {
        // 1 Get state
        Dictionary<string, object> state = LoadFile(saveFile);
        if (state.ContainsKey("lastSceneBuildIndex"))
        {
            int sceneBuildIndex = (int)state["lastSceneBuildIndex"];

            // 2 Load Last Scene
            if (sceneBuildIndex != SceneManager.GetActiveScene().buildIndex)
            {
                yield return SceneManager.LoadSceneAsync(sceneBuildIndex);
            }
        }

        // 3 Restore state
        RestoreState(state);
    }

    private void SaveFile(string saveFile, object state)
    {
        string path = GetPathFromSaveFile(saveFile);
        using (FileStream fileStream = File.Open(path, FileMode.Create))
        {
            binaryFormatter.Serialize(fileStream, state);
            /// Always close your file stream as the OS has the file open.
            /// fileStream.Close(); 
            /// the using keyword does this for us.
        }
    }

    private Dictionary<string, object> LoadFile(string saveFile)
    {
        string path = GetPathFromSaveFile(saveFile);
        if (!File.Exists(path))
        {
            // Debug.Log("No Save File Currently Exists");
            return new Dictionary<string, object>();
        }
        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            object stateObject = binaryFormatter.Deserialize(fileStream);
            Dictionary<string, object> state = (Dictionary<string, object>)stateObject;
            return state;
            /// Always close your file stream as the OS has the file open.
            /// fileStream.Close(); 
            /// the using keyword does this for us.
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveableEntity in FindObjectsOfType<SaveableEntity>())
        {
            state[saveableEntity.GetUniqueIdentifier()] = saveableEntity.CaptureState();
        }

        state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveableEntity in FindObjectsOfType<SaveableEntity>())
        {
            string uniqueID = saveableEntity.GetUniqueIdentifier();
            if (state.ContainsKey(uniqueID))
            {
                saveableEntity.RestoreState(state[uniqueID]);
            }
        }
    }

    private string GetPathFromSaveFile(string saveFile)
    {
        // Debug.Log("Application.persistentDataPath: " + Application.persistentDataPath);
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            // Debug.Log("Directory Does Not Exist, creating Directory...");
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }
        // This doesn't work, only God and Microsoft know why.
        // string path = Path.Combine(Application.persistentDataPath, "/saves/" + saveFile + ".mySaveFile"); 
        // This works.
        string path = Application.persistentDataPath + "/saves/" + saveFile + ".mySaveFile";
        // Debug.Log("Path: " + path);
        return path;
    }

}