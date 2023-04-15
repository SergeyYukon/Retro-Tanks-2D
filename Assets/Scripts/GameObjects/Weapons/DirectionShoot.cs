using UnityEngine;

namespace Components
{
    public abstract class DirectionShoot : MonoBehaviour
    {
        protected void RotateToDirection(Vector3 direction, GameObject gun)
        {
            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
        }
    }
}
