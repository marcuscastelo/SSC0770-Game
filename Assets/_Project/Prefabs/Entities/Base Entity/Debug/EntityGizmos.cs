using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class EntityGizmos : MonoBehaviour
{
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private Transform entityTransform;

    void Awake()
    {
        entityTransform = gameObject.GetComponentInParent<Entity>().transform;
    }

    void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(entityTransform.position, 0.1f);
    }
}