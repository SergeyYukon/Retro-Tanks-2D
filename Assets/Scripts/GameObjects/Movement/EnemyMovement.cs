using UnityEngine;
using UnityEngine.AI;

namespace Components.Enemy
{
    public class EnemyMovement : Movement
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _forwardPoint;
        private Transform _baseTransform;
        private Transform _player1Transform;
        private Transform _player2Transform;
        private Transform _currentTarget;
        private float _distanceToAttackTarget;
        private EnemyType _type;
        private bool _isAttackingEnemy;

        public void Construct(Transform baseTransform, Transform player1Transform, Transform player2Transform, float movementSpeed, float rotationSpeed,
            float distanceToAttackTarget, EnemyType type)
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            _baseTransform = baseTransform;
            _player1Transform = player1Transform;
            _player2Transform = player2Transform;
            _agent.speed = movementSpeed;
            _rotationSpeed = rotationSpeed;
            _distanceToAttackTarget = distanceToAttackTarget;
            _type = type;

            StartTarget();
        }

        private void Update()
        {
            if (_isAttackingEnemy)
            {
                _currentTarget = AttackPlayerTarget(_player1Transform, _player2Transform);
            }

            if (DistanceToTarget(_currentTarget) < _distanceToAttackTarget)
            {
                _agent.isStopped = true;
            }
            else
            {
                _agent.isStopped = false;
            }
        }

        private void FixedUpdate()
        {
            if (_currentTarget != null)
            {
                Movement();
                RotationTo();
            }
        }

        public override void ChangeSpeed(float amount)
        {
            _agent.speed += amount;
        }

        private void Movement()
        {                
            _agent.SetDestination(_currentTarget.position);           
        }

        private void RotationTo()
        {
            if (Direction(_agent.velocity) != Vector3.zero)
            {
                Rotation(Direction(_agent.velocity));
            }
            else
            {
                Rotation(DirectionToTarget(_currentTarget));
            }
        }

        private void StartTarget()
        {
            if (_type == EnemyType.AttackPlayerEnemy)
            {
                _isAttackingEnemy = true;
                _currentTarget = AttackPlayerTarget(_player1Transform, _player2Transform);
            }
            else
            {
                _currentTarget = _baseTransform;
            }
        }
    }
}
