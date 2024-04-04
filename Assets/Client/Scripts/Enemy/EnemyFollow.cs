using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyFollow : MonoBehaviour
    {
        public Action<PlayerHurtbox> OnNearPlayer;
        public Action OnFarPlayer;

        [SerializeField] private float _speed;
        private GameObject _chaseTarget;
        private bool _chase;
        private bool _attackCalled;

        private void Update()
        {
            Chase();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out PlayerBehavior player))
            {
                _chase = true;
                _chaseTarget = player.gameObject;
            }
            else if (col.gameObject.TryGetComponent(out PlayerHurtbox playerHurtbox))
            {
                _chase = false;
                if (!_attackCalled)
                {
                    OnNearPlayer?.Invoke(playerHurtbox);
                    _attackCalled = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerBehavior player))
            {
                _chase = false;
                _chaseTarget = null;
                OnFarPlayer?.Invoke();
            }
            else if (other.gameObject.TryGetComponent(out PlayerHurtbox playerHurtbox))
            {
                _chase = true;
                _attackCalled = false;
                OnFarPlayer?.Invoke();
            }
        }

        private void Chase()
        {
            if (_chaseTarget)
            {
                Vector2 position = transform.position;
                Vector2 chasePosition = _chaseTarget.transform.position;

                Vector2 direction = chasePosition - position;
                direction.Normalize();

                if (_chase)
                {
                    transform.position = Vector2.MoveTowards(position,
                        chasePosition, _speed * Time.deltaTime);
                }
            }
        }
    }
}