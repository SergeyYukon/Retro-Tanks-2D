using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public abstract class Health : MonoBehaviour
    {
        public abstract void GetDamage(float damage);
    }
}
