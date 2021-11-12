using UnityEngine;
using System.Collections.Generic;

public class EntityMotor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EntityController controller;

    private readonly List<IMovementModifier> modifiers = new List<IMovementModifier>();

    public void AddModifier(IMovementModifier modifier) => modifiers.Add(modifier);
    public void RemoveModifier(IMovementModifier modifier) => modifiers.Remove(modifier);

    private void Update() => Move();

    private void Move()
    {
        Vector3 movement = Vector3.zero;

        foreach (IMovementModifier modifier in modifiers)
        {
            movement += modifier.Value;
        }

        controller.Move(movement * Time.deltaTime);
    }

}