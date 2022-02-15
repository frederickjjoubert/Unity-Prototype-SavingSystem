using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, ISaveable
{

    #region Attributes

    private MeshRenderer _meshRenderer;

    [System.Serializable]
    struct CubeSaveData
    {
        public SerializableVector3 position;
        public SerializableVector3 rotation;
        public SerializableColor color;
    }

    #endregion Attributes

    #region ISaveable Conformance

    public object CaptureState()
    {
        CubeSaveData cubeSaveData = new CubeSaveData();
        cubeSaveData.position = new SerializableVector3(transform.position);
        cubeSaveData.rotation = new SerializableVector3(transform.eulerAngles);
        cubeSaveData.color = new SerializableColor(_meshRenderer.material.color);
        return cubeSaveData;
    }

    public void RestoreState(object state)
    {
        CubeSaveData cubeSaveData = (CubeSaveData)state;
        transform.position = cubeSaveData.position.ToVector();
        transform.eulerAngles = cubeSaveData.rotation.ToVector();
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

    #endregion Unity Lifecycle
}
