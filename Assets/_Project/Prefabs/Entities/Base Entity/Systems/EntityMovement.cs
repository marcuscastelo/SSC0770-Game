using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Core;
using Hypnos.Entities;
using Zenject;

namespace Hypnos.Entities.Systems
{
    //TODO: use strategy pattern for movement (Walk, Dash)
    public class EntityMovement : MonoBehaviour, IMoveable
    {
        private Entity _entity;

        public WalkStats WalkStats => _entity.WalkStats;
        public DashStats DashStats => _entity.DashStats;

        public Vector2 Position { get { return _entity.Rigidbody.position; } }
        public Vector2 CurrentVelocity { get; private set; }
        public bool VelocitySetManually { get; private set; } //* Represents DASH (for now it's a little bit of a hack)

        private Vector2 _targetVelocity;
        private float _acceleration;

        [Inject]
        public void Construct(Entity entity)
        {
            _entity = entity;
        }

        #region IMoveable
        public void Teleport(Vector2 position)
        {
            _entity.Rigidbody.position = position;
        }

        public void SetVel(Vector2 velocity)
        {
            VelocitySetManually = true;
            CurrentVelocity = velocity;

            _targetVelocity = CurrentVelocity;
            _acceleration = 0;
        }

        public void AccelerateTo(Vector2 targetVel, float accel)
        {
            VelocitySetManually = false;
            _targetVelocity = targetVel;
            _acceleration = accel;
        }
        #endregion

        #region Unity Functions

        private void FixedUpdate()
        {
            if (_acceleration > 0)
                CurrentVelocity = Vector2.MoveTowards(CurrentVelocity, _targetVelocity, _acceleration * Time.fixedDeltaTime);

            bool capVelocity = !VelocitySetManually;
            if (capVelocity)
                CurrentVelocity = Vector2.ClampMagnitude(CurrentVelocity, WalkStats.maxSpeed);

            _entity.Rigidbody.MovePosition(_entity.Rigidbody.position + CurrentVelocity * Time.fixedDeltaTime);
        }
        #endregion


    }
}