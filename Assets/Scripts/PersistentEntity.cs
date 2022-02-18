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
        public List<PersistentEntityComponentData> persistentEntityComponentDatas;
    }

    [Serializable]
    public class PersistentEntityComponentData
    {
        public string name;
        public string state;
    }

    public PersistentEntityData CaptureState()
    {
        PersistentEntityData persistentEntityData = new PersistentEntityData();
        persistentEntityData.prefabName = prefabName;

        List<PersistentEntityComponentData> persistentEntityComponentDatas = new List<PersistentEntityComponentData>();

        foreach (ISaveable saveable in GetComponents<ISaveable>()) // Add all ISaveables from child components in the future?
        {
            PersistentEntityComponentData persistentEntityComponentData = new PersistentEntityComponentData();
            persistentEntityComponentData.name = saveable.GetType().ToString(); // These are the Components on the GameObject
            persistentEntityComponentData.state = saveable.CaptureState();
            persistentEntityComponentDatas.Add(persistentEntityComponentData);
        }
        persistentEntityData.persistentEntityComponentDatas = persistentEntityComponentDatas;

        return persistentEntityData;
    }

    public void RestoreState(PersistentEntityData persistentEntityData)
    {
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString(); // These are the Components on the GameObject

            for (int i = 0; i < persistentEntityData.persistentEntityComponentDatas.Count; i++)
            {
                PersistentEntityComponentData persistentEntityComponentData = persistentEntityData.persistentEntityComponentDatas[i];
                if (persistentEntityComponentData.name == typeName)
                {
                    saveable.RestoreState(persistentEntityComponentData.state);
                }
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
