using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected float _rotationSpeed;

    public abstract void ChangeSpeed(float amount);

    protected void Rotation(Vector3 direction)
    {
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}
