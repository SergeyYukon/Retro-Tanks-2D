using Path;
using System;
using UnityEngine;

namespace Infrastructure.Services
{
    public class LootGeneratorService 
    {
        private enum LootType { Empty, Heal, UpgradeMoveSpeed, UpgradeCooldownShoot }
        private LootType lootType;

        public void GetLoot(Vector3 position)
        {
            lootType = RandomLoot();
            switch (lootType)
            {
                case LootType.Empty:
                    break;
                case LootType.Heal:
                    UnityEngine.Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.HealBonusPath), position, Quaternion.identity);
                    break;
                case LootType.UpgradeMoveSpeed:
                    UnityEngine.Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.UpgradeMoveSpeedBonusPath), position, Quaternion.identity);
                    break;
                case LootType.UpgradeCooldownShoot:
                    UnityEngine.Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.UpgradeCooldownShootBonusPath), position, Quaternion.identity);
                    break;
            }
        }

        private LootType RandomLoot()
        {
            int emptyOrNot = UnityEngine.Random.Range(1, 101);
            if(emptyOrNot < 50)
            {
                int algorithmCount = Enum.GetNames(typeof(LootType)).Length;
                int random = UnityEngine.Random.Range(0, algorithmCount);
                return (LootType)random;
            }
            return 0;
        }
    }
}
