using Components.Weapon;
using UnityEngine;

namespace Bonus
{
    public class UpgradeDamage : MonoBehaviour
    {
        [SerializeField] private float amount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<Shooter>().UpgradeDamage(amount);
            Destroy(gameObject);
        }
    }
}
