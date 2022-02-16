using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways] // This tells Unity to execute Update in Edit Time and Run Time
public class SaveableEntity : MonoBehaviour
{

    [SerializeField] string uniqueIdentifier = "";

    public static Dictionary<string, SaveableEntity> globalIDLookup = new Dictionary<string, SaveableEntity>();

    public string GetUniqueIdentifier()
    {
        return uniqueIdentifier;
    }

    public object CaptureState()
    {
        //Debug.Log("Capturing state for: " + GetUniqueIdentifier());
        Dictionary<string, object> state = new Dictionary<string, object>();
        foreach (ISaveable saveable in GetComponents<ISaveable>()) // Add all ISaveables from child components in the future?
        {
            string typeName = saveable.GetType().ToString(); // Ex. One of the saveables is going to be a 'Mover'.
            state[typeName] = saveable.CaptureState();
        }
        return state;
    }

    public void RestoreState(object state)
    {
        //Debug.Log("Restoring state for: " + GetUniqueIdentifier());
        Dictionary<string, object> stateDictionary = (Dictionary<string, object>)state;
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString(); // Ex. One of the saveables is going to be a 'Mover'.
            if (stateDictionary.ContainsKey(typeName))
            {
                saveable.RestoreState(stateDictionary[typeName]);
            }
        }
    }

    #region Unity Lifecylce

    private void Start()
    {
        SerializedObject serializedObject = new SerializedObject(this); // This monobehavior
        SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

        if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
        {
            property.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();
        }

        globalIDLookup[property.stringValue] = this; // This current SaveableEntity.
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.IsPlaying(gameObject)) return;
        if (string.IsNullOrEmpty(gameObject.scene.path)) return; // An empty path means we are in a prefab

        SerializedObject serializedObject = new SerializedObject(this); // This monobehavior
        SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

        if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
        {
            property.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();
        }

        globalIDLookup[property.stringValue] = this; // This current SaveableEntity.
    }
#endif

    #endregion Unity Lifecycle

    #region Private Functions

    private bool IsUnique(string stringValue)
    {
        if (!globalIDLookup.ContainsKey(stringValue)) return true;
        if (globalIDLookup[stringValue] == this) return true;

        if (globalIDLookup[stringValue] == null) // It has been deleted
        {
            globalIDLookup.Remove(stringValue);
            return true;
        }

        if (globalIDLookup[stringValue].GetUniqueIdentifier() != stringValue) // In case you created a new ID in the editor
        {
            globalIDLookup.Remove(stringValue);
            return true;
        }

        return false;
    }

    #endregion Private Functions

}