using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public event Action<InputType, InputArg> OnInputReceived;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OnInputReceived?.Invoke(InputType.Esc, new InputArg(true));
        }

        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            OnInputReceived?.Invoke(InputType.Esc, new InputArg(false));
        }

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

        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            OnInputReceived?.Invoke(InputType.Mouse_Middle_Pressed, new InputArg(true));
        }

        if (Mouse.current.middleButton.wasReleasedThisFrame)
        {
            OnInputReceived?.Invoke(InputType.Mouse_Middle_Released, new InputArg(false));
        }
    }

    public Vector2 MousePosition => Mouse.current.position.ReadValue();
    public Vector2 MouseScroll => Mouse.current.scroll.ReadValue();
}
