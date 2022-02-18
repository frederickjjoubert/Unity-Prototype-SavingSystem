
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeRandomDataOne : MonoBehaviour, ISaveable
{

    // Let's Pretend this is the mover.

    #region Attributes

    public float speed = 5;

    [Serializable]
    struct MoverSaveData
    {
        public Vector3 position;
        public Vector3 rotation;
        public float speed;
    }

    #endregion Attributes

    #region Conformance to ISaveable

    public string CaptureState()
    {
        MoverSaveData moverSaveData = new MoverSaveData();
        moverSaveData.position = transform.position;
        moverSaveData.rotation = transform.eulerAngles;
        moverSaveData.speed = speed;
        return JsonUtility.ToJson(moverSaveData);
    }

    // Note: RestoreState is called after Awake() but before Start()
    public void RestoreState(string state)
    {
        MoverSaveData moverSaveData = JsonUtility.FromJson<MoverSaveData>(state);
        transform.position = moverSaveData.position;
        transform.eulerAngles = moverSaveData.rotation;
        speed = moverSaveData.speed;
    }

    #endregion Conformance to ISaveable


}
