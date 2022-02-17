using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateParser : MonoBehaviour
{

    #region Attributes

    [SerializeField] private Sphere sphere;
    [SerializeField] private EnemySpawner enemySpawner;

    #endregion Attributes

    #region Public Functions

    public StateData CaptureState()
    {
        StateData stateData = new StateData();

        StateMetaData stateMetaData = new StateMetaData();
        stateMetaData.saveID = "saveID";
        stateMetaData.lastSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        PlayerSaveData playerSaveData = new PlayerSaveData();
        playerSaveData.position = sphere.gameObject.transform.position;
        stateData.playerSaveData = playerSaveData;

        foreach (Cube cube in FindObjectsOfType<Cube>())
        {
            // state[saveableEntity.GetUniqueIdentifier()] = saveableEntity.CaptureState();
            EnemySaveData enemySaveData = new EnemySaveData();
            enemySaveData.uuid = System.Guid.NewGuid().ToString();
            enemySaveData.position = cube.gameObject.transform.position;
            stateData.enemySaveDatas.Add(enemySaveData);
        }

        return stateData;
    }

    public StateData GenerateDummyStateData()
    {
        StateData stateData = new StateData();

        StateMetaData stateMetaData = new StateMetaData();
        stateMetaData.saveID = "someUniqueID";
        stateMetaData.lastSceneBuildIndex = 1;

        PlayerSaveData playerSaveData = new PlayerSaveData();
        playerSaveData.position = Vector3.one * Random.value;
        stateData.playerSaveData = playerSaveData;

        EnemySaveData enemySaveData1 = new EnemySaveData();
        enemySaveData1.uuid = "enemy1";
        enemySaveData1.position = Vector3.one * Random.value;
        EnemySaveData enemySaveData2 = new EnemySaveData();
        enemySaveData2.uuid = "enemy2";
        enemySaveData2.position = Vector3.one * Random.value;
        EnemySaveData enemySaveData3 = new EnemySaveData();
        enemySaveData3.uuid = "enemy3";
        enemySaveData3.position = Vector3.one * Random.value;
        stateData.enemySaveDatas.Add(enemySaveData1);
        stateData.enemySaveDatas.Add(enemySaveData2);
        stateData.enemySaveDatas.Add(enemySaveData3);

        return stateData;
    }

    public void RestoreState(StateData stateData)
    {
        Debug.Log("RestoreState");

        PlayerSaveData playerSaveData = stateData.playerSaveData;
        sphere.gameObject.transform.position = playerSaveData.position;

        enemySpawner.RestoreState(stateData.enemySaveDatas);
    }

    #endregion Public Functions

}
