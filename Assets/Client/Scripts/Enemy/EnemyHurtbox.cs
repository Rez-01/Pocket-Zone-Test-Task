using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHurtbox : MonoBehaviour
    {
        public Action<int> OnBullet;
    }
}