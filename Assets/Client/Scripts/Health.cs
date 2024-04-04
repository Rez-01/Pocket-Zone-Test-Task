using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Action OnZeroHealth;
    
    [SerializeField] private int _maxHealth;
    private Slider _slider;
    private int _currentHealth;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _slider.maxValue = _maxHealth;
        _slider.value = _currentHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnZeroHealth?.Invoke();
        }
        
        UpdateHealth(_currentHealth);
    }

    private void UpdateHealth(int health)
    {
        _slider.value = _currentHealth;
    }
}
