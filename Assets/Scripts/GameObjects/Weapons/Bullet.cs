using Components;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private float _layerNumberToAttack;
    private float _layerNumberToImmunity;

    public void Construct(float damage, LayerMask layerToAttack, LayerMask immunity)
    {
        _damage = damage;

        _layerNumberToAttack = Mathf.Log(layerToAttack.value, 2);
        _layerNumberToImmunity = Mathf.Log(immunity.value, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == _layerNumberToAttack)
        {
            collision.GetComponentInParent<Health>().GetDamage(_damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer != _layerNumberToImmunity)
        {
            Destroy(gameObject);
        }
    }
}
