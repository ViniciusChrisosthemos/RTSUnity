using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("Configuração")]
    [SerializeField] private Camera m_camera;
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private bool invert = false;
    [SerializeField] private float m_minZoom = 5f;
    [SerializeField] private float m_maxZoom = 10f;
    [SerializeField] private float m_zoomSpeed = 0.5f;

    private Vector3 lastMouseWorldPos;
    private bool isDragging = false;

    private InputManager m_inputManager;

    private void Start()
    {
        m_inputManager = InputManager.Instance;

        m_inputManager.OnInputReceived += HandleInputReceived;

        m_camera.orthographicSize = m_minZoom;
    }

    private void Update()
    {
        // Arrasta a câmera
        if (isDragging)
        {
            Vector3 currentMouseWorldPos = GetMouseWorldPosition();
            Vector3 delta = lastMouseWorldPos - currentMouseWorldPos;

            if (invert)
                delta = -delta;

            m_camera.transform.position += delta * dragSpeed;

            lastMouseWorldPos = GetMouseWorldPosition();
        }

        if (m_inputManager.MouseScroll.y != 0)
        {
            m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize - m_inputManager.MouseScroll.y * m_zoomSpeed, m_minZoom, m_maxZoom);

        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = InputManager.Instance.MousePosition;

        // Importante para câmera ortográfica 2D
        mouseScreenPos.z = -m_camera.transform.position.z;

        Vector3 worldPos = m_camera.ScreenToWorldPoint(mouseScreenPos);
        worldPos.z = transform.position.z; // mantém Z da câmera
        return worldPos;
    }

    private void StartMoving()
    {
        isDragging = true;
        lastMouseWorldPos = GetMouseWorldPosition();
    }

    private void HandleInputReceived(InputType inputType, InputArg arg)
    {
        switch (inputType)
        {
            case InputType.Mouse_Middle_Pressed: StartMoving(); break;
            case InputType.Mouse_Middle_Released: isDragging = false; break;
            default: break;
        }
    }

    public Camera Camera => m_camera;
}
