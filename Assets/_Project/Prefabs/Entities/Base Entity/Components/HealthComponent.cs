using System;
using UnityEngine;

using Zenject;
namespace Hypnos.Entities.Components
{
    [Serializable]
    public class HealthComponent
    {
        private int _maxHealth = 1;
        private int _currentHealth = 1;

        public HealthComponent()
        {
            //TODO: inject maxHealth from EntityStats
        }

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;

        public event Action OnDeath = delegate { };

        public void TakeDamage(int damage)
        {
            SetHealth(_currentHealth - damage);
        }

        public void Heal(int heal)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + heal, 0, _maxHealth);
        }

        public void SetHealth(int health)
        {
            _currentHealth = Mathf.Clamp(health, 0, _maxHealth);
            if (_currentHealth == 0)
            {
                OnDeath();
            }
        }

        public void SetMaxHealth(int newMax)
        {
            _maxHealth = Mathf.Clamp(newMax, 0, int.MaxValue);
            if (_currentHealth > _maxHealth)
                SetHealth(_maxHealth);
        }

        public void Die() => SetHealth(0);
    }
}