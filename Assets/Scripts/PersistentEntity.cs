using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentEntity : MonoBehaviour
{

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
