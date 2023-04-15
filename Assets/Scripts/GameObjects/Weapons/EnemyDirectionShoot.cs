using UnityEngine;

namespace Components
{
    public class EnemyDirectionShoot : DirectionShoot
    {
        [SerializeField] private GameObject gun;
        private Transform _playerTransform;
        public void Construct(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Update()
        {
            RotateToDirection(_playerTransform.position, gun);
        }
    }
}
