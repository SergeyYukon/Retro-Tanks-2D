using Components.Player;
using UnityEngine;

namespace Components
{
    public class UpgradeMoveSpeed : MonoBehaviour
    {
        [SerializeField] private float amount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<PlayerMovement>().UpgradeSpeed(amount);
            Destroy(gameObject);
        }
    }
}
