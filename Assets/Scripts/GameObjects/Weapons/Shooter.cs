using Infrastructure.Data;
using Infrastructure.Factory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Weapon
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        private GameObjectsFactory _gameObjectsFactory;
        private float _damage;
        private float _pushPower;
        private float _cooldownShoot;
        private LayerMask _layerToAttack;
        private GameObject _bulletPrefab;
        private LayerMask _immunity;
        private GameData _gameData;
        private const float minCooldownShoot = 0.2f;

        private float _startCooldownShoot;

        public void Construct(GameObjectsFactory gameObjectsFactory, float damage, float pushPower,
            float cooldownShoot, LayerMask layerToAttack, GameObject bulletPrefab, LayerMask immunity, GameData gameData)
        {
            _gameObjectsFactory = gameObjectsFactory;
            _gameData = gameData;
            _damage = damage;
            _pushPower = pushPower;
            _cooldownShoot = _startCooldownShoot = cooldownShoot - _gameData.UpgradeCooldownShoot;
            _startCooldownShoot = _startCooldownShoot = Mathf.Max(_startCooldownShoot, minCooldownShoot);
            _layerToAttack = layerToAttack;
            _bulletPrefab = bulletPrefab;
            _immunity = immunity;
        }

        private void Update()
        {
            if (ReadyShoot())
            {
                Shoot();
                _cooldownShoot = _startCooldownShoot;
            }
        }

        public void UpgradeCooldownShoot(float amount)
        {
            _gameData.UpgradeCooldownShoot += amount;
            _startCooldownShoot -= _gameData.UpgradeCooldownShoot;
            _startCooldownShoot = Mathf.Max(_startCooldownShoot, minCooldownShoot);
        }

        private void Shoot()
        {
            Vector2 direction = (_firePoint.position - transform.position).normalized;
            GameObject bullet = _gameObjectsFactory.CreateBullet(_firePoint.position, _damage, _layerToAttack, _bulletPrefab, _immunity);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * _pushPower, ForceMode2D.Impulse);
        }

        private bool ReadyShoot()
        {
            _cooldownShoot -= Time.deltaTime;
            return _cooldownShoot <= 0;
        }
    }
}
