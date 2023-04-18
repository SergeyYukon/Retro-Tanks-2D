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

    protected float DistanceToTarget(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector3 DirectionToTarget(Transform target)
    {
        Vector3 direction = new Vector3(target.position.y - transform.position.y, -(target.position.x - transform.position.x), 0).normalized;
        return direction;
    }

    protected Vector3 Direction(Vector2 velocity)
    {
        Vector2 destination = velocity;
        Vector3 direction = new Vector3(destination.y, -destination.x, 0).normalized;
        return direction;
    }

    protected Transform AttackPlayerTarget(Transform player1, Transform player2)
    {
        if (player2 != null)
        {
            if (DistanceToTarget(player1) < DistanceToTarget(player2))
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
        else
        {
            return player1;
        }
    }
}
