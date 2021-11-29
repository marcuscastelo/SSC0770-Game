using System;
using Hypnos.Core;

using Hypnos.Entities;

namespace Hypnos.Entities.Components
{
    [Serializable]
    public class BuffComponent : IBuffable
    {
        private Entity _entity;

        public BuffComponent(Entity entity)
        {
            _entity = entity;
        }

        public Buff ActiveBuff { get; private set; } = Buff.NoItem;

        public void ApplyBuff(Buff buff) { 
            if (buff == Buff.Defense && !HasBuff(Buff.Defense)) {
                _entity.Health.SetMaxHealth(_entity.Health.MaxHealth + 1);
                _entity.Health.SetHealth(_entity.Health.CurrentHealth + 1);
            }
            ActiveBuff |= buff;
        }
        public void RemoveBuff(Buff buff) { 
            if (buff == Buff.Defense && HasBuff(Buff.Defense)) {
                _entity.Health.SetMaxHealth(_entity.Health.MaxHealth - 1);
            }
            ActiveBuff &= ~buff;
        }
        public void ClearBuffs() => ActiveBuff = Buff.NoItem;
        public bool HasBuff(Buff buff) => (ActiveBuff & buff) == buff;
    }
}