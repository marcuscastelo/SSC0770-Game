using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace Hypnos.Entities
{
    [CustomEditor(typeof(Entity))]
    public class EntityEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Entity entity = (Entity)target;

            if (entity.Animator == null) return; //Zenject have not yet injected the animator

            Buff buff = entity.ActiveBuff;
    
            Buff newBuff = (Buff)EditorGUILayout.EnumFlagsField(buff);
            entity.ClearBuffs();
            entity.ApplyBuff(newBuff);

            EditorGUILayout.LabelField("Stats");
        }
    }
}
#endif