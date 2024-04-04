using UnityEngine;

namespace Player
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private GameObject _gun;

        private PlayerShoot _playerShoot;
        private GameObject _target;

        private void OnEnable()
        {
            _playerShoot.OnAim += StartAim;
            _playerShoot.StopAim += StopAim;
        }

        private void OnDisable()
        {
            _playerShoot.OnAim -= StartAim;
            _playerShoot.StopAim -= StopAim;
        }

        private void Awake()
        {
            _playerShoot = GetComponent<PlayerShoot>();
        }

        private void Update()
        {
            if (_target) Aim();
        }

        private void StartAim(GameObject target)
        {
            _target = target;
        }

        private void StopAim()
        {
            _target = null;
        }
    
        private void Aim()
        {
            _gun.transform.right = _target.transform.position - _gun.transform.position;
        }
    }
}