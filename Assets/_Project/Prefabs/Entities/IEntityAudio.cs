using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypnos.Entities
{
    public interface IEntityAudio<T> where T : Entity
    {
        void PlayAttackSound(T entity);
        void PlayDeathSound(T entity);
        void PlayWalkSound(T entity);
        void PlayDashSound(T entity);
    }
}