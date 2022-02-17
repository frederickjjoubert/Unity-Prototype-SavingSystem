using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentEntityLoader : MonoBehaviour
{

    public void SpawnPersistentEntities(List<PersistentEntity.PersistentEntityData> persistentEntityDatas)
    {
        foreach (PersistentEntity.PersistentEntityData persistentEntityData in persistentEntityDatas)
        {
            string path = $"Prefabs/{persistentEntityData.prefabName}";
            GameObject targetPrefab = Resources.Load<GameObject>(path);
            float x = persistentEntityData.position.x;
            float z = persistentEntityData.position.z;
            Instantiate(targetPrefab, new Vector3(x, 0, z), Quaternion.identity);
        }
    }
}
