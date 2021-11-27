using System;
using UnityEngine;

using Zenject;
namespace Hypnos.Entities.Components
{
    [Serializable]
    public class HealthComponent
    {
        private int maxHealth = 100;
        private int currentHealth = 100;
        private bool invulnerable = false;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        private SignalBus _signalBus;

        public void TakeDamage(int damage)
        {
            if (invulnerable)
                return;

            SetHealth(currentHealth - damage);
        }

        public void Heal(int heal)
        {
            currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        }

        public void SetHealth(int health)
        {
            currentHealth = Mathf.Clamp(health, 0, maxHealth);
            if (currentHealth == 0)
            {
                
            }
        }

        public void SetMaxHealth(int newMax)
        {
            maxHealth = Mathf.Clamp(newMax, 0, int.MaxValue);
            if (currentHealth > maxHealth)
                SetHealth(maxHealth);
        }

        public void Die() => SetHealth(0);
    }
}