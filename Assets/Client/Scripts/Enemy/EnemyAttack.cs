using System.Collections;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _attackInterval;

        private EnemyFollow _enemyFollow;
        private bool _canAttack;
        private PlayerHurtbox _playerHurtbox;

        private void Awake()
        {
            _enemyFollow = GetComponent<EnemyFollow>();
        }

        private void OnEnable()
        {
            _enemyFollow.OnNearPlayer += StartAttack;
            _enemyFollow.OnFarPlayer += StopAttack;
        }

        private void OnDisable()
        {
            _enemyFollow.OnNearPlayer -= StartAttack;
            _enemyFollow.OnFarPlayer -= StopAttack;
        }

        private void StartAttack(PlayerHurtbox playerHurtbox)
        {
            _playerHurtbox = playerHurtbox;
            _canAttack = true;
            StartCoroutine(Attack());
        }

        private void StopAttack()
        {
            _canAttack = false;
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(_attackInterval);
                if (_canAttack)
                {
                    _playerHurtbox.OnAttacked?.Invoke(_damage);
                }
            }
        }
    }
}