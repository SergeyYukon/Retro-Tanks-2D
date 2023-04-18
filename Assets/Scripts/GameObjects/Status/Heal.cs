using Components;
using UnityEngine;

namespace Bonus
{
    public class Heal : MonoBehaviour
    {
        [SerializeField] private float amount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<PlayerHealth>().GetHeal(amount);
            Destroy(gameObject);
        }
    }
}
