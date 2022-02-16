using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    #region Attributes

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    #endregion Attributes

    #region Public Functions

    public void Spawn()
    {
        Debug.Log("Spawn");
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomZ = UnityEngine.Random.Range(minZ, maxZ);
        Instantiate(enemyPrefab, new Vector3(randomX, 0, randomZ), Quaternion.identity);
    }

    #endregion Public Functions

}
