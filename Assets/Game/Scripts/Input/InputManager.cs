using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [Header("Depencencies")]
    [SerializeField] private CameraManager m_cameraManager;

    [Header("Parameters")]
    [SerializeField] private LayerMask m_uiLayerMask;

    public event Action<InputType, InputArg> OnInputReceived;

    private void Update()
    {
        if (!IsPointerInUI())
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                OnInputReceived?.Invoke(InputType.Mouse_Left_Pressed, new InputArg(Mouse.current.position.ReadValue()));
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                OnInputReceived?.Invoke(InputType.Mouse_Left_Released, new InputArg(Mouse.current.position.ReadValue()));
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                OnInputReceived?.Invoke(InputType.Mouse_Right_Pressed, new InputArg(Mouse.current.position.ReadValue()));
            }

            if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                OnInputReceived?.Invoke(InputType.Mouse_Right_Released, new InputArg(Mouse.current.position.ReadValue()));
            }
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OnInputReceived?.Invoke(InputType.Esc, new InputArg(true));
        }

        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            OnInputReceived?.Invoke(InputType.Esc, new InputArg(false));
        }

        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            OnInputReceived?.Invoke(InputType.Mouse_Middle_Pressed, new InputArg(true));
        }

        if (Mouse.current.middleButton.wasReleasedThisFrame)
        {
            OnInputReceived?.Invoke(InputType.Mouse_Middle_Released, new InputArg(false));
        }
    }

    public Ray m_ray;
    public bool IsPointerInUI()
    {

        return EventSystem.current.IsPointerOverGameObject();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(m_ray.origin, m_ray.direction * 1000);
    }

    public Vector2 MousePosition => Mouse.current.position.ReadValue();
    public Vector2 MouseScroll => Mouse.current.scroll.ReadValue();
}
