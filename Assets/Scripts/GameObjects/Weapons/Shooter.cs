using Infrastructure.Data;
using Infrastructure.Factory;
using UnityEngine;

namespace Components.Weapon
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private AudioSource bonusSound;
        [SerializeField] private Transform _firePoint;
        private GameObjectsFactory _gameObjectsFactory;
        private float _damage;
        private float _pushPower;
        private float _currentCooldownShoot;
        private LayerMask _layerToAttack;
        private GameObject _bulletPrefab;
        private LayerMask _immunity;
        private GameData _gameData;

        private float _startCooldownShoot;

        public void Construct(GameObjectsFactory gameObjectsFactory, float damage, float pushPower,
            float cooldownShoot, LayerMask layerToAttack, GameObject bulletPrefab, LayerMask immunity, GameData gameData)
        {
            _gameObjectsFactory = gameObjectsFactory;
            _gameData = gameData;

            _damage = damage + _gameData.UpgradeDamage;
            _damage = Mathf.Min(_damage, _gameData.MaxUpgradeDamage);
            _gameData.CurrentDamage = _damage;

            _pushPower = pushPower;

            _startCooldownShoot = cooldownShoot - _gameData.UpgradeCooldownShoot;
            _currentCooldownShoot = _startCooldownShoot = Mathf.Max(_startCooldownShoot, _gameData.MinCooldownShoot);
            _gameData.CurrentCooldown = _startCooldownShoot;

            _layerToAttack = layerToAttack;
            _bulletPrefab = bulletPrefab;
            _immunity = immunity;
        }

        private void Update()
        {
            if (ReadyShoot())
            {
                Shoot();
                _currentCooldownShoot = _startCooldownShoot;
            }
        }

        public void UpgradeCooldownShoot(float amount)
        {
            bonusSound.Play();
            _gameData.UpgradeCooldownShoot += amount;
            _startCooldownShoot -= amount;
            _startCooldownShoot = Mathf.Max(_startCooldownShoot, _gameData.MinCooldownShoot);
            _gameData.CurrentCooldown = _startCooldownShoot;
        }

        public void UpgradeDamage(float amount)
        {
            bonusSound.Play();
            _gameData.UpgradeDamage += amount;
            _damage += amount;
            _damage = Mathf.Min(_damage, _gameData.MaxUpgradeDamage);
            _gameData.CurrentDamage = _damage;
        }

        private void Shoot()
        {
            Vector2 direction = (_firePoint.position - transform.position).normalized;
            GameObject bullet = _gameObjectsFactory.CreateBullet(_firePoint.position, _damage, _layerToAttack, _bulletPrefab, _immunity);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * _pushPower, ForceMode2D.Impulse);
        }

        private bool ReadyShoot()
        {
            _currentCooldownShoot -= Time.deltaTime;
            return _currentCooldownShoot <= 0;
        }
    }
}
