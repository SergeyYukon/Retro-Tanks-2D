using UnityEngine;

namespace Components
{
    public class Swamp : MonoBehaviour
    {
        private const float amountChangeSpeed = 0.5f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponentInParent<Movement>().ChangeSpeed(-amountChangeSpeed);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            collision.gameObject.GetComponentInParent<Movement>().ChangeSpeed(amountChangeSpeed);
        }
    }
}
