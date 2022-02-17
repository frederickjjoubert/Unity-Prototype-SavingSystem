using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : SingletonMonobehavior<StateController>
{

    #region Attributes

    const string defaultSaveFile = "SaveFileName";

    private JSONSaveSystem savingSystem;
    private StateParser stateParser;

    // Component References
    private GameController gameController;

    #endregion Attributes

    #region Public Functions

    public void Save()
    {
        // StateData stateData = stateParser.GenerateDummyStateData();
        StateData stateData = stateParser.CaptureState();
        savingSystem.Save(defaultSaveFile, stateData);
    }

    public void Load()
    {
        StateData stateData = savingSystem.Load(defaultSaveFile);
        stateParser.RestoreState(stateData);
    }

    public void Delete()
    {
        GetComponent<SavingSystem>().Delete(defaultSaveFile);
    }

    #endregion Public Functions

    #region Unity Lifecycle

    private void Awake()
    {
        Debug.Log("StateController Awake");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        savingSystem = GetComponent<JSONSaveSystem>();
        stateParser = GetComponent<StateParser>();
        // StartCoroutine(LoadLastScene());
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.J))
        // {
        //     Save();
        // }
        // if (Input.GetKeyDown(KeyCode.K))
        // {
        //     Load();
        // }
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     Delete();
        // }
    }

    #endregion Unity Lifecycle

    #region Private Functions

    // private IEnumerator LoadLastScene()
    // {
    //     yield return savingSystem.LoadLastScene(defaultSaveFile);
    // }

    #endregion Private Functions
}
