using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypnos.Core
{
    public interface IBuffable
    {
        Buff ActiveBuff { get; }
        event System.Action<Buff> OnBuffAddedEvent;
        event System.Action<Buff> OnBuffRemovedEvent;

        void ApplyBuff(Buff buff);
        void RemoveBuff(Buff buff);
        void SetBuff(Buff buff);

        bool HasBuff(Buff buff);

        void ClearBuffs();
    }
}