using UnityEngine;
using UnityEngine.AI;

namespace Components.Enemy
{
    public class EnemyMovement : Movement
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _forwardPoint;
        private Transform _baseTransform;
        private Transform _currentTarget;
        private const float _distanceToBase = 3;

        public void Construct(Transform baseTransform,float movementSpeed, float rotationSpeed)
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            _baseTransform = baseTransform;
            _agent.speed = movementSpeed;
            _rotationSpeed = rotationSpeed;

            _currentTarget = _baseTransform;
        }

        private void Update()
        {             
          
            if(DistanceToBase() > _distanceToBase)               
            {
                _currentTarget = _baseTransform;            
            }
            else
            {
                _agent.isStopped = true;
            }
        }

        private void FixedUpdate()
        {
            Movement();
            RotationTo();
        }

        public override void ChangeSpeed(float amount)
        {
            _agent.speed += amount;
        }

        private void Movement()
        {            
            _agent.SetDestination(_currentTarget.position);
        }

        private Vector3 Direction()
        {
            Vector2 destination = _agent.velocity;
            Vector3 direction = new Vector3(destination.y, -destination.x, 0).normalized;
            return direction;
        }

        private Vector3 DirectionToBase()
        {
            Vector3 direction = new Vector3(_currentTarget.position.y - transform.position.y, -(_currentTarget.position.x - transform.position.x), 0).normalized;
            return direction;
        }

        private void RotationTo()
        {
            if (Direction() != Vector3.zero)
            {
                Rotation(Direction());
            }
            else
            {
                Rotation(DirectionToBase());
            }
        }

        private float DistanceToBase()
        {
            return Vector3.Distance(transform.position, _baseTransform.position);
        }
    }
}
