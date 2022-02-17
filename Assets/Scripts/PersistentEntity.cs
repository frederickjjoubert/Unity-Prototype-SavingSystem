using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentEntity : MonoBehaviour
{

    [SerializeField] private string prefabName;

    [Serializable]
    public class PersistentEntityData
    {
        public string prefabName;
        public Vector3 position;
        public object state;
    }

    public PersistentEntityData CaptureState()
    {
        PersistentEntityData persistentEntityData = new PersistentEntityData();
        persistentEntityData.prefabName = prefabName;
        persistentEntityData.position = transform.position;

        Dictionary<string, object> state = new Dictionary<string, object>();
        foreach (ISaveable saveable in GetComponents<ISaveable>()) // Add all ISaveables from child components in the future?
        {
            string typeName = saveable.GetType().ToString(); // Ex. One of the saveables is going to be a 'Mover'.
            state[typeName] = saveable.CaptureState();
        }
        persistentEntityData.state = state;

        return persistentEntityData;
    }

    public void RestoreState(object state)
    {
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

    #region Unity Lifecycle

    private void OnEnable()
    {
        GameController.Instance.OnBeforeStateChanged += HandleBeforeGameStateChanged;
        GameController.Instance.OnAfterStateChanged += HandleAfterGameStateChanged;
    }

    private void OnDisable()
    {
        GameController.Instance.OnBeforeStateChanged -= HandleBeforeGameStateChanged;
        GameController.Instance.OnAfterStateChanged -= HandleAfterGameStateChanged;
    }

    #endregion Unity Lifecycle

    #region Handler Functions

    private void HandleBeforeGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Loading)
        {
            Destroy(gameObject);
        }
    }

    private void HandleAfterGameStateChanged(GameState gameState)
    {
        // Do Nothing
    }

    #endregion Handler Functions
}
