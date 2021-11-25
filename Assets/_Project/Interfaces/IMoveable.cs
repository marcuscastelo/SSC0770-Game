using UnityEngine;

public interface IMoveable 
{
    Vector2 CurrentVelocity { get; }
    Vector2 Position { get; }
    
    void Teleport(Vector2 position);
    void SetVel(Vector2 velocity);
    void AccelerateTo(Vector2 targetVel, float accel);
}
