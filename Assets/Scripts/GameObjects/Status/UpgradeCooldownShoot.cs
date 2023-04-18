using Components.Weapon;
using UnityEngine;

namespace Bonus
{
    public class UpgradeCooldownShoot : MonoBehaviour
    {
        [SerializeField] private float amount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<Shooter>().UpgradeCooldownShoot(amount);
            Destroy(gameObject);
        }
    }
}
