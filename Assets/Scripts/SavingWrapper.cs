using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingWrapper : MonoBehaviour
{

    const string defaultSaveFile = "SaveFileName";

    private SavingSystem savingSystem;

    private void Awake()
    {
        savingSystem = GetComponent<SavingSystem>();
        // StartCoroutine(LoadLastScene());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Delete();
        }
    }

    public void Save()
    {
        savingSystem.Save(defaultSaveFile);
    }

    public void Load()
    {
        savingSystem.Load(defaultSaveFile);
    }

    public void Delete()
    {
        savingSystem.Delete(defaultSaveFile);
    }
}