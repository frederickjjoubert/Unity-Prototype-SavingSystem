using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, ISaveable
{

    #region Attributes

    private MeshRenderer _meshRenderer;

    [System.Serializable]
    public struct CubeSaveData
    {
        public Vector3 position;
        public Quaternion rotation;
        public SerializableColor color;
    }

    #endregion Attributes

    #region ISaveable Conformance

    public object CaptureState()
    {
        CubeSaveData cubeSaveData = new CubeSaveData();
        cubeSaveData.position = transform.position;
        cubeSaveData.rotation = transform.rotation;
        cubeSaveData.color = new SerializableColor(_meshRenderer.material.color);
        return cubeSaveData;
    }

    public void RestoreState(object state)
    {
        CubeSaveData cubeSaveData = (CubeSaveData)state;
        transform.position = cubeSaveData.position;
        transform.eulerAngles = cubeSaveData.rotation.eulerAngles;
        _meshRenderer.material.color = cubeSaveData.color.ToColor();
    }

    #endregion ISaveable Conformance

    #region Unity Lifecycle

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Color
        if (Input.GetKeyDown(KeyCode.R))
        {
            float r = Random.value;
            float g = Random.value;
            float b = Random.value;
            Color randomColor = new Color(r, g, b);
            _meshRenderer.material.color = randomColor;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy: " + gameObject.name);
    }

    #endregion Unity Lifecycle



}
