using UnityEngine;
using UnityEngine.AI;

namespace Components.Enemy
{
    public class EnemyMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _forwardPoint;
        private float _rotationSpeed;
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
            Rotation();
        }

        public void ChangeSpeed(float amount)
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

        private void Rotation()
        {
            if (Direction() != Vector3.zero)
            {
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * Direction();
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            }
            else
            {
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * DirectionToBase();
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            }
        }

        private float DistanceToBase()
        {
            return Vector3.Distance(transform.position, _baseTransform.position);
        }
    }
}
