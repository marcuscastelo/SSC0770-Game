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

            if (Application.isPlaying) 
                PlayModeOnInspectorGUI();
        }

        private void PlayModeOnInspectorGUI()
        {
            Entity entity = (Entity)target;
            Buff buff = entity.ActiveBuff;
    
            Buff buffs = (Buff)EditorGUILayout.EnumFlagsField(buff);
            entity.SetBuff(buffs);

            EditorGUILayout.LabelField("Health", entity.Health.CurrentHealth.ToString());
        }
    }
}
#endif