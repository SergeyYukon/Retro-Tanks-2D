using UnityEngine;

namespace Components
{
    public class PlayerDirectionShoot : DirectionShoot
    {
        [SerializeField] private GameObject gun;
        private Camera _cameraMain;

        private void Start()
        {
            _cameraMain = Camera.main;
        }

        private void Update()
        {
            Vector3 direction = (_cameraMain.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            RotateToDirection(direction, gun);
        }
    }
}
