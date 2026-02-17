using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    public event System.Action<Vector2> OnInputDown;
    public event System.Action<Vector2> OnInputUp;

    public void OnClick(InputValue value)
    {
        //Debug.Log($"InputManager received click input with value: {value.isPressed}");
        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (value.isPressed)
        {
            OnInputDown?.Invoke(mousePos);
        } else
        {
            OnInputUp?.Invoke(mousePos);
        }
    }
}
