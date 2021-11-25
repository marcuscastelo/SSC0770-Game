using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypnos.Entities.Components
{
    public class BuffComponent : MonoBehaviour, IBuffable
    {
        [SerializeField] private Buff activeBuff = Buff.NoItem;

        public Buff ActiveBuff => activeBuff;
        public void ApplyBuff(Buff buff) => activeBuff |= buff;
        public void RemoveBuff(Buff buff) => activeBuff &= ~buff;
        public void ClearBuffs() => activeBuff = Buff.NoItem;
    }
}