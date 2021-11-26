using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Core;

namespace Hypnos.Entities.Components
{
    public class BuffComponent : MonoBehaviour, IBuffable
    {
        [SerializeField] private Buff activeBuff = Buff.NoItem;

        public delegate void OnBuffChanged(Buff buff);
        public event OnBuffChanged OnBuffChangedEvent = delegate { };

        public Buff ActiveBuff
        {
            get
            {
                return activeBuff;
            }
            set
            {
                activeBuff = value;
                OnBuffChangedEvent(activeBuff);
            }
        }
        
        public void ApplyBuff(Buff buff) => activeBuff |= buff;
        public void RemoveBuff(Buff buff) => activeBuff &= ~buff;
        public void ClearBuffs() => activeBuff = Buff.NoItem;
        public bool HasBuff(Buff buff) => (activeBuff & buff) == buff;
    }
}