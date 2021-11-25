using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypnos.Entities.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 100;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        public delegate void OnHealthChanged(int newHealth);
        public event OnHealthChanged OnHealthChangedEvent;

        public delegate void OnMaxHealthChanged(int newMaxHealth);
        public event OnMaxHealthChanged OnMaxHealthChangedEvent;

        public delegate void OnDeath();
        public event OnDeath OnDeathEvent;

        public delegate void OnDamageTaken(int damage);
        public event OnDamageTaken OnDamageTakenEvent;

        public delegate void OnHealed(int heal);
        public event OnHealed OnHealedEvent;

        public void TakeDamage(int damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
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
        }

        public void SetMaxHealth(int health)
        {
            maxHealth = health;
            OnMaxHealthChangedEvent?.Invoke(maxHealth);
        }
    }
}