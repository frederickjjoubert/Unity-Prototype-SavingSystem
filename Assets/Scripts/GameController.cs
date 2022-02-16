using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region Attributes

    // Events
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    // State
    public GameState State { get; private set; }

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
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Saving:
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
