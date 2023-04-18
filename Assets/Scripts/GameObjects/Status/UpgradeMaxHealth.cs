using Components;
using UnityEngine;

namespace Bonus
{
    public class UpgradeMaxHealth : MonoBehaviour
    {
        [SerializeField] private float amount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<PlayerHealth>().UpgradeMaxHealth(amount);
            Destroy(gameObject);
        }
    }
}
