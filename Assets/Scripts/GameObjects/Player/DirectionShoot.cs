using UnityEngine;

namespace Components
{
    public class DirectionShoot : MonoBehaviour
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
            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
        }
    }
}
