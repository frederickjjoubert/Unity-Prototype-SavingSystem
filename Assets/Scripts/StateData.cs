using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateData
{
    public float myLevel;
    public string myID;
    public int[] myArray;

    public StateMetaData stateMetaData;
    public PlayerSaveData playerSaveData;
    public List<EnemySaveData> enemySaveDatas = new List<EnemySaveData>();
}

[Serializable]
public class StateMetaData
{
    public string saveID;
    public int lastSceneBuildIndex;
}

[Serializable]
public class PlayerSaveData
{
    public Vector3 position;
}

[Serializable]
public class EnemySaveData
{
    public string uuid;
    public Vector3 position;
}
