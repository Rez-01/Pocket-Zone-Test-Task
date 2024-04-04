using Enemy;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHurtbox hurtbox))
            {
                hurtbox.OnBullet?.Invoke(_damage);
                Destroy(gameObject);
            }
        }
    }
}