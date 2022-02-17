using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    #region Attributes

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float minX = -8;
    [SerializeField] private float maxX = 8;
    [SerializeField] private float minZ = -4;
    [SerializeField] private float maxZ = 4;

    #endregion Attributes

    #region Public Functions

    public void Spawn()
    {
        Debug.Log("Spawn");
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomZ = UnityEngine.Random.Range(minZ, maxZ);
        Instantiate(enemyPrefab, new Vector3(randomX, 0, randomZ), Quaternion.identity);
    }

    public void RestoreState(List<EnemySaveData> enemySaveDatas)
    {
        foreach (EnemySaveData enemySaveData in enemySaveDatas)
        {
            float x = enemySaveData.position.x;
            float z = enemySaveData.position.z;
            Instantiate(enemyPrefab, new Vector3(x, 0, z), Quaternion.identity);
        }
    }

    #endregion Public Functions

}
