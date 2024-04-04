using UnityEngine;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyHurtbox _hurtbox;

        private EnemyItemDrop _itemDrop;

        private void OnEnable()
        {
            _hurtbox.OnBullet += _health.TakeDamage;
            _health.OnZeroHealth += Die;
        }

        private void OnDisable()
        {
            _hurtbox.OnBullet -= _health.TakeDamage;
            _health.OnZeroHealth -= Die;
        }

        private void Awake()
        {
            _itemDrop = GetComponent<EnemyItemDrop>();
        }

        private void Die()
        {
            _itemDrop.ItemDrop();
            Destroy(gameObject);
        }
    }
}