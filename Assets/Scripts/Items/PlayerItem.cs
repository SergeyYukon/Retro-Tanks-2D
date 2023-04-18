using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "PlayerItem", menuName = "Items/PlayerItem")]
    public class PlayerItem : ScriptableObject
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float health;
        [SerializeField] private float damage;
        [SerializeField] private float pushPowerShoot;
        [SerializeField] private float cooldownShoot;
        [SerializeField] private LayerMask layerToAttack;
        [SerializeField] private LayerMask immunity;

        public GameObject PlayerPrefab => playerPrefab;
        public GameObject BulletPrefab => bulletPrefab;
        public float MovementSpeed => movementSpeed;
        public float RotationSpeed => rotationSpeed;
        public float Health => health;
        public float Damage => damage;
        public float PushPowerShoot => pushPowerShoot;
        public float CooldownShoot => cooldownShoot;
        public LayerMask LayerToAttack => layerToAttack;
        public LayerMask Immunity => immunity;
    }
}
