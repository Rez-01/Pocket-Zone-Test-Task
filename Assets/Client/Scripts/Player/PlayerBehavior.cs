using UnityEngine;

namespace Player
{
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerHurtbox _hurtbox;
    
        private void OnEnable()
        {
            _hurtbox.OnAttacked += _health.TakeDamage;
            _health.OnZeroHealth += Die;
        }

        private void OnDisable()
        {
            _hurtbox.OnAttacked -= _health.TakeDamage;
            _health.OnZeroHealth -= Die;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }   
}