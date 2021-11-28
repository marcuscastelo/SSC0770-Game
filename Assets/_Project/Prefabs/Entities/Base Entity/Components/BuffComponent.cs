using System;
using Hypnos.Core;

namespace Hypnos.Entities.Components
{
    [Serializable]
    public class BuffComponent : IBuffable
    {
        public Buff ActiveBuff { get; private set; } = Buff.NoItem;
        
        public void ApplyBuff(Buff buff) => ActiveBuff |= buff;
        public void RemoveBuff(Buff buff) => ActiveBuff &= ~buff;
        public void ClearBuffs() => ActiveBuff = Buff.NoItem;
        public bool HasBuff(Buff buff) => (ActiveBuff & buff) == buff;
    }
}