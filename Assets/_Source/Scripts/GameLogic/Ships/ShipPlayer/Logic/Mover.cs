using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputPlayer))]
public class Mover : MonoBehaviour
{
    private float _speed;
    private float _gravity = 20;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _characterController;
    private InputPlayer _inputPlayer;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputPlayer = GetComponent<InputPlayer>();
    }

    private void Update()
    {
        Move();
    }

    public void Init(float speed)
    {
        _speed = speed;
    }

    public void Move()
    {
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
        else
        {
            Vector2 inputDirection = _inputPlayer.TakeDirection();
            _moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
            _moveDirection = transform.TransformDirection(_moveDirection) * _speed;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}
