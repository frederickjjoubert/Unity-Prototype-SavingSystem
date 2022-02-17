using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour, ISaveable
{

    [SerializeField] private float speed = 2;
    private float input_x;
    private float input_y;

    private Vector3 movement_input = Vector3.zero;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    [System.Serializable]
    struct SphereSaveData
    {
        public Vector3 position;
        public Quaternion rotation;
        public SerializableColor color;
    }

    #region ISaveable Conformance

    public object CaptureState()
    {
        SphereSaveData sphereSaveData = new SphereSaveData();
        sphereSaveData.position = transform.position;
        sphereSaveData.rotation = transform.rotation;
        sphereSaveData.color = new SerializableColor(_meshRenderer.material.color);
        return sphereSaveData;
    }

    public void RestoreState(object state)
    {
        SphereSaveData sphereSaveData = (SphereSaveData)state;
        transform.position = sphereSaveData.position;
        transform.eulerAngles = sphereSaveData.rotation.eulerAngles;
        _meshRenderer.material.color = sphereSaveData.color.ToColor();
    }

    #endregion ISaveable Conformance

    #region Unity Lifecycle

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        // Movement
        input_x = Input.GetAxis("Horizontal");
        input_y = Input.GetAxis("Vertical");
        movement_input = new Vector3(input_x, 0, input_y).normalized;

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

    private void OnEnable()
    {
        GameController.Instance.OnBeforeStateChanged += HandleBeforeGameStateChanged;
        GameController.Instance.OnAfterStateChanged += HandleAfterGameStateChanged;
    }

    private void FixedUpdate()
    {
        // _rigidbody.MovePosition(transform.position + movement_input * speed * Time.fixedDeltaTime);
        if (movement_input.magnitude > 0.1)
        {
            _rigidbody.AddForce(movement_input * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    #endregion Unity Lifecycle

    #region Handler Functions

    private void HandleBeforeGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Playing:
                _rigidbody.velocity = Vector3.zero;
                break;
            default:
                break;
        }
    }

    private void HandleAfterGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            default:
                break;
        }
    }

    #endregion Handler Functions
}
