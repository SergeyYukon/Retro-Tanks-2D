using UnityEngine;
using UnityEngine.AI;

namespace Components.Enemy
{
    public class PatrolEnemyMovement : Movement
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _forwardPoint;
        private Transform _player1Transform;
        private Transform _player2Transform;
        private Transform _currentTarget;
        private float _distanceToAttackTarget;
        private float _distanceToPlayerTarget;
        private float _patrolDistance;
        private bool _isHavePatrolTarget;
        private GameObject _instance;

        public void Construct(Transform player1Transform, Transform player2Transform, float movementSpeed, float rotationSpeed,
            float distanceToAttackTarget, float patrolDistance, float distanceToPlayerTarget)
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            _player1Transform = player1Transform;
            _player2Transform = player2Transform;
            _agent.speed = movementSpeed;
            _rotationSpeed = rotationSpeed;
            _distanceToAttackTarget = distanceToAttackTarget;
            _distanceToPlayerTarget = distanceToPlayerTarget;
            _patrolDistance = patrolDistance;
        }

        private void Update()
        {
            PatrolMovement();

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

        private void Movement()
        {
            _agent.SetDestination(_currentTarget.position);
        }

        private void PatrolMovement()
        {
            if (_player2Transform != null)
            {
                if (DistanceToTarget(_player1Transform) < _distanceToPlayerTarget || DistanceToTarget(_player2Transform) < _distanceToPlayerTarget)
                {
                    _currentTarget = AttackPlayerTarget(_player1Transform, _player2Transform);
                }
                else
                {
                    if (!_isHavePatrolTarget)
                    {
                        _currentTarget = SetTarget();
                    }
                    else
                    {
                        _agent.SetDestination(_currentTarget.position);

                        if (DistanceToTarget(_currentTarget) < _distanceToAttackTarget)
                        {
                            _isHavePatrolTarget = false;
                        }
                    }
                }
            }
            else
            {
                if (DistanceToTarget(_player1Transform) < _distanceToPlayerTarget)
                {
                    _currentTarget = _player1Transform;
                }
                else
                {
                    if (!_isHavePatrolTarget)
                    {
                        _currentTarget = SetTarget();
                    }
                    else
                    {
                        _agent.SetDestination(_currentTarget.position);

                        if (DistanceToTarget(_currentTarget) < _distanceToAttackTarget)
                        {
                            _isHavePatrolTarget = false;
                        }
                    }
                }
            }
        }

        private Vector3 SetRandomTarget()
        {
            bool isPathCorrect = false;
            Vector2 currentTarget = Vector2.zero;
            while (!isPathCorrect)
            {
                if (NavMesh.SamplePosition(Random.insideUnitSphere * _patrolDistance + transform.position, out NavMeshHit hit, _patrolDistance, NavMesh.AllAreas))
                {
                    isPathCorrect = true;
                    currentTarget = hit.position;
                }
            }
            return currentTarget;
        }

        private Transform SetTarget()
        {
            if (_instance != null)
            {
                Destroy(_instance);
            }

            _instance = Instantiate(new GameObject("target"), SetRandomTarget(), Quaternion.identity);
            Transform target = _instance.transform;
            _isHavePatrolTarget = true;
            return target;
        }
    }
}
