using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypnos.Entities.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 100;
        [SerializeField] private bool invulnerable = false;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        public delegate void OnHealthChanged(int newHealth);
        public event OnHealthChanged OnHealthChangedEvent = delegate { };

        public delegate void OnMaxHealthChanged(int newMaxHealth);
        public event OnMaxHealthChanged OnMaxHealthChangedEvent = delegate { };

        public delegate void OnDeath();
        public event OnDeath OnDeathEvent = delegate { };

        public delegate void OnDamageTaken(int damage);
        public event OnDamageTaken OnDamageTakenEvent = delegate { };

        public delegate void OnHealed(int heal);
        public event OnHealed OnHealedEvent = delegate { };

        public void TakeDamage(int damage)
        {
            if (invulnerable)
                return;

            SetHealth(currentHealth - damage);
            OnDamageTakenEvent?.Invoke(damage);
        }

        public void Heal(int heal)
        {
            currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
            OnHealedEvent?.Invoke(heal);
        }

        public void SetHealth(int health)
        {
            currentHealth = Mathf.Clamp(health, 0, maxHealth);
            OnHealthChangedEvent?.Invoke(currentHealth);
            if (currentHealth == 0)
                OnDeathEvent?.Invoke();
        }

        public void SetMaxHealth(int newMax)
        {
            maxHealth = Mathf.Clamp(newMax, 0, int.MaxValue);
            OnMaxHealthChangedEvent?.Invoke(maxHealth);
            if (currentHealth > maxHealth)
                SetHealth(maxHealth);
        }

        public void Die() => SetHealth(0);
    }
}