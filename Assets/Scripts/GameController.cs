using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMonobehavior<GameController>
{

    #region Attributes

    // Events
    public event Action<GameState> OnBeforeStateChanged;
    public event Action<GameState> OnAfterStateChanged;

    // State
    public GameState State { get; private set; }
    [SerializeField] private StateController stateController;

    #endregion Attributes

    #region Public Functions

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                break;
            case GameState.Loading:
                stateController.Load();
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Saving:
                stateController.Save();
                break;
            case GameState.Quitting:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }

    #endregion Public Functions

    #region Unity Lifecycle

    private void Start()
    {
        ChangeState(GameState.Starting);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Save
            ChangeState(GameState.Saving);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Load
            ChangeState(GameState.Loading);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Delete
            stateController.Delete();
        }
    }

    #endregion Unity Lifecycle

}

[Serializable]
public enum GameState
{
    // There is ambiguity between Starting and Playing, I think I need one state for GameActive or something. Playing sounds good.
    Starting = 0,
    Loading = 1,
    Playing = 2,
    Paused = 3,
    Saving = 4,
    Quitting = 5
}
