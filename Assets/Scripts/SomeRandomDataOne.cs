
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
        public SerializableVector3 position;
        public SerializableVector3 rotation;
        public float speed;
    }

    #endregion Attributes

    #region Conformance to ISaveable

    public object CaptureState()
    {
        MoverSaveData moverSaveData = new MoverSaveData();
        moverSaveData.position = new SerializableVector3(transform.position);
        moverSaveData.rotation = new SerializableVector3(transform.eulerAngles);
        moverSaveData.speed = speed;
        return moverSaveData;
    }

    // Note: RestoreState is called after Awake() but before Start()
    public void RestoreState(object state)
    {
        MoverSaveData moverSaveData = (MoverSaveData)state;
        transform.position = moverSaveData.position.ToVector();
        transform.eulerAngles = moverSaveData.rotation.ToVector();
        speed = moverSaveData.speed;
    }

    #endregion Conformance to ISaveable


}
