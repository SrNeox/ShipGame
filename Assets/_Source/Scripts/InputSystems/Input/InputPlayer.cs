using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private InputSystem _playerInput;

    private void Awake()
    {
        _playerInput = new InputSystem();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
         _playerInput.Disable();
    }

    

    public Vector2 TakeDirection() => _playerInput.Player.Move.ReadValue<Vector2>();

    public bool Shoot() => _playerInput.Player.Shoot.triggered;

    public bool Draw() => _playerInput.Player.Draw.IsPressed();

    public bool Clear() => _playerInput.Player.Clear.triggered;
}
