using System;
using UnityEngine;

namespace Player
{
    public class PlayerHurtbox : MonoBehaviour
    {
        public Action<int> OnAttacked;
    }
}
