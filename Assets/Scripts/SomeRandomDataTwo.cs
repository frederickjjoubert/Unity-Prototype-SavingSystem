using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeRandomDataTwo : MonoBehaviour, ISaveable
{

    // Let's pretend this is the stats

    #region Attributes

    public int strength = 10;
    public int agility = 10;
    public int constitution = 10;
    public int wisdom = 10;
    public int ego = 10;

    [Serializable]
    struct StatsSaveData
    {
        public int strength;
        public int agility;
        public int constitution;
        public int wisdom;
        public int ego;
    }

    #endregion Attributes

    #region Conformance to ISaveable

    public object CaptureState()
    {
        StatsSaveData statsSaveData = new StatsSaveData();
        statsSaveData.strength = strength;
        statsSaveData.agility = agility;
        statsSaveData.constitution = constitution;
        statsSaveData.wisdom = wisdom;
        statsSaveData.ego = ego;
        return statsSaveData;
    }

    // Note: RestoreState is called after Awake() but before Start()
    public void RestoreState(object state)
    {
        StatsSaveData statsSaveData = (StatsSaveData)state;
        strength = statsSaveData.strength;
        agility = statsSaveData.agility;
        constitution = statsSaveData.constitution;
        wisdom = statsSaveData.wisdom;
        ego = statsSaveData.ego;
    }

    #endregion Conformance to ISaveable
}
