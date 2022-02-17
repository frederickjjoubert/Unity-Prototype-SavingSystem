using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StateFileManager : MonoBehaviour
{
    #region Attributes

    public string[] saveFiles;

    #endregion Attributes

    #region Public Functions

    public string[] GetStateFiles()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
        return saveFiles;
    }

    #endregion Public Functions

    #region Unity Lifecycle

    private void Start()
    {
        GetStateFiles();
    }

    #endregion Unity Lifecycle

}
