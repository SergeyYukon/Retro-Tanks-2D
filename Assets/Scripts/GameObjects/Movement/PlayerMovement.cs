using UnityEngine;
using Infrastructure.Services;
using Infrastructure.Data;

namespace Components.Player
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private Rigidbody2D rb;
        private InputService _input;
        private GameData _gameData;
        private Vector3 _rotateDirection;
        private float _movementSpeed;
        private float _rotationSpeed;

        public void Construct(InputService input, float movementSpeed, float rotationSpeed, GameData gameData)
        {
            _input = input;
            _gameData = gameData;
            _movementSpeed = movementSpeed + _gameData.UpgradeMoveSpeed;
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
                Rotation();
            }
        }

        public void ChangeSpeed(float amount)
        {
            _movementSpeed += amount;
        }

        public void UpgradeSpeed(float amount)
        {
            _gameData.UpgradeMoveSpeed += amount;
            _movementSpeed += amount;
        }

        private void Movement()
        {
            rb.MovePosition(rb.position + _input.Axis * _movementSpeed * Time.fixedDeltaTime);
        }

        private void Rotation()
        {
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * _rotateDirection;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
