using UnityEngine;
using Infrastructure.Services;
using Infrastructure.Data;

namespace Components.Player
{
    public class PlayerMovement : Movement
    {
        [SerializeField] private AudioSource bonusSound;
        [SerializeField] private Rigidbody2D rb;
        private IInputService _input;
        private GameData _gameData;
        private Vector3 _rotateDirection;
        private float _movementSpeed;

        public void Construct(IInputService input, float movementSpeed, float rotationSpeed, GameData gameData)
        {
            _input = input;
            _gameData = gameData;

            _movementSpeed = movementSpeed + _gameData.UpgradeMoveSpeed;
            _movementSpeed = Mathf.Min(_movementSpeed, _gameData.MaxSpeed);
            _gameData.CurrentSpeed = _movementSpeed;
            _rotationSpeed = rotationSpeed;
        }

        private void Update()
        {
            _rotateDirection = new Vector3(_input.Axis.y, -_input.Axis.x, 0);
        }

        private void FixedUpdate()
        {
            if (_input.Axis.magnitude > 0.1f)
            {                  
                Movement();                  
                Rotation(_rotateDirection);
            }
        }

        public override void ChangeSpeed(float amount)
        {
            _movementSpeed += amount;
        }

        public void UpgradeSpeed(float amount)
        {
            bonusSound.Play();
            _gameData.UpgradeMoveSpeed += amount;
            _movementSpeed += amount;
            _movementSpeed = Mathf.Min(_movementSpeed, _gameData.MaxSpeed);
            _gameData.CurrentSpeed = _movementSpeed;
        }

        private void Movement()
        {
            rb.MovePosition(rb.position + _input.Axis * _movementSpeed * Time.fixedDeltaTime);
        }
    }
}
