using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Core;

namespace Hypnos.Entities.Systems
{
    public class EntityMovement : MonoBehaviour, IMoveable
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private WalkStats walkStats;
        [SerializeField] private DashStats dashStats;

        public WalkStats WalkStats => walkStats;
        public DashStats DashStats => dashStats;

        public Vector2 Position { get { return rb.position; } }
        public Vector2 CurrentVelocity { get; private set; }
        public bool IsDashing { get; private set; }

        private Vector2 _targetVelocity;
        private float _acceleration;


        #region Hacky Events (not ideal, but I am too overwhelmed refactoring this by the xth time now)
        public void OnDashStart() => IsDashing = true;
        public void OnDashEnd() => IsDashing = false;
        #endregion


        #region IMoveable
        public void Teleport(Vector2 position)
        {
            rb.position = position;
        }

        public void SetVel(Vector2 velocity)
        {
            CurrentVelocity = velocity;

            _targetVelocity = CurrentVelocity;
            _acceleration = 0;
        }

        public void AccelerateTo(Vector2 targetVel, float accel)
        {
            _targetVelocity = targetVel;
            _acceleration = accel;
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            if (walkStats == null)
                walkStats = new WalkStats();


            Debug.Assert(rb != null, "EntityMovement.Awake() - rb is null");
            Debug.Assert(walkStats != null, "EntityMovement.Awake() - movementStats is null");
        }

        private void FixedUpdate()
        {
            if (_acceleration > 0)
                CurrentVelocity = Vector2.MoveTowards(CurrentVelocity, _targetVelocity, _acceleration * Time.fixedDeltaTime);

            bool capVelocity = !IsDashing;
            if (capVelocity)
                CurrentVelocity = Vector2.ClampMagnitude(CurrentVelocity, walkStats.maxSpeed);

            rb.MovePosition(rb.position + CurrentVelocity * Time.fixedDeltaTime);
        }
        #endregion


    }
}