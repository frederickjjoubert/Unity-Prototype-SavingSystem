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
            GameObject instantiatedObject = Instantiate(targetPrefab);
            PersistentEntity persistentEntity = instantiatedObject.GetComponent<PersistentEntity>();
            persistentEntity.RestoreState(persistentEntityData);
        }
    }
}
