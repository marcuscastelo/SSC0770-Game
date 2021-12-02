using System;
using Hypnos.Core;

using Hypnos.Entities;

namespace Hypnos.Entities.Components
{
    [Serializable]
    public class BuffComponent : IBuffable
    {
        private Entity _entity;
        public event Action<Buff> OnBuffAddedEvent = delegate { };
        public event Action<Buff> OnBuffRemovedEvent = delegate { };

        public BuffComponent(Entity entity)
        {
            _entity = entity;
        }

        public Buff ActiveBuff { get; private set; } = Buff.None;

        public void ApplyBuff(Buff buff)
        {
            Buff addedBuffs = (~ActiveBuff) & buff;
            if (addedBuffs != Buff.None)
                OnBuffAddedEvent.Invoke(addedBuffs);
            ActiveBuff |= buff;
        }

        public void RemoveBuff(Buff buff)
        {
            Buff removedBuffs = ActiveBuff & buff;
            if (removedBuffs != Buff.None)
                OnBuffRemovedEvent.Invoke(removedBuffs);
            ActiveBuff &= ~buff;
        }

        public void SetBuff(Buff buff)
        {
            Buff addedBuffs = (~ActiveBuff) & buff;
            Buff removedBuffs = ActiveBuff & (~buff);
            if (addedBuffs != Buff.None)
                OnBuffAddedEvent.Invoke(addedBuffs);

            if (removedBuffs != Buff.None)
                OnBuffRemovedEvent.Invoke(removedBuffs);

            ActiveBuff = buff;
        }

        public void ClearBuffs() => RemoveBuff(ActiveBuff);

        public bool HasBuff(Buff buff) => ActiveBuff.HasFlag(buff);
    }
}