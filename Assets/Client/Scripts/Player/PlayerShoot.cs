using System;
using System.Collections;
using Enemy;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        public Action<GameObject> OnAim;
        public Action StopAim;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private int _reloadTime;
        [SerializeField] private int _maxBulletCount;
        [SerializeField] private TMP_Text _bulletCountText;

        private bool _canShoot;
        private int _currentBulletCount;

        private void Awake()
        {
            _currentBulletCount = _maxBulletCount;
            UpdateBulletCount();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHurtbox enemyHurtbox))
            {
                _canShoot = true;
                OnAim?.Invoke(enemyHurtbox.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHurtbox enemyHurtbox))
            {
                _canShoot = false;
                StopAim?.Invoke();
            }
        }

        public void Shoot()
        {
            if (_canShoot && _currentBulletCount > 0)
            {
                GameObject bullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.right * 5, ForceMode2D.Impulse);
                _currentBulletCount -= 1;

                if (_currentBulletCount <= 0)
                {
                    StartCoroutine(Reload());
                }
                else
                {
                    UpdateBulletCount();
                }
            }
        }

        private void UpdateBulletCount()
        {
            _bulletCountText.text = _currentBulletCount.ToString();
        }

        private IEnumerator Reload()
        {
            _bulletCountText.text = "...";

            yield return new WaitForSeconds(_reloadTime);

            _currentBulletCount = _maxBulletCount;
            UpdateBulletCount();
        }
    }
}