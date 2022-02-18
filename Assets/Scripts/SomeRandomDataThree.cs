using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeRandomDataThree : MonoBehaviour, ISaveable
{
    // Let's pretend this is the stats

    #region Attributes

    public List<Item> items = new List<Item>();

    [Serializable]
    struct ItemsSaveData
    {
        public List<Item> items;
    }

    #endregion Attributes

    #region Conformance to ISaveable

    public string CaptureState()
    {
        ItemsSaveData itemsSaveData = new ItemsSaveData();
        itemsSaveData.items = items;
        return JsonUtility.ToJson(itemsSaveData);
    }

    // Note: RestoreState is called after Awake() but before Start()
    public void RestoreState(string state)
    {
        ItemsSaveData itemsSaveData = JsonUtility.FromJson<ItemsSaveData>(state);
        items = itemsSaveData.items;
    }

    #endregion Conformance to ISaveable

    #region Unity Lifecycle

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Item item = new Item();
            item.id = "item" + i;
            item.quantity = 50;
            items.Add(item);
        }
    }

    #endregion Unity Lifecycle

}

[Serializable]
public class Item
{
    public string id;
    public int quantity;
}
