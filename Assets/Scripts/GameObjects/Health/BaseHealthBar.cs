using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class BaseHealthBar : MonoBehaviour
    {
        [SerializeField] private BaseHealth health;
        [SerializeField] private Image healthFill;

        private void Start()
        {
            health.OnHealthChanged += UpdateBar;
        }

        private void UpdateBar(float currentHealth, float maxHealth)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }

        private void OnDestroy()
        {
            health.OnHealthChanged -= UpdateBar;
        }
    } 
}
