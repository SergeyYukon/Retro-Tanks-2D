using Components.Enemy;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "EnemyItem", menuName = "Items/EnemyItem")]
    public class EnemyItem : ScriptableObject
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private EnemyType type;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float health;
        [SerializeField] private float damage;
        [SerializeField] private float pushPowerShoot;
        [SerializeField] private float cooldownShoot;
        [SerializeField] private LayerMask layerToAttack;
        [SerializeField] private LayerMask immunity;

        public GameObject EnemyPrefab => enemyPrefab;
        public GameObject BulletPrefab => bulletPrefab;
        public EnemyType Type => type;
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
