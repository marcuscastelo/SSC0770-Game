using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    Buff ActiveBuff { get; }
    
    void ApplyBuff(Buff buff);
    void RemoveBuff(Buff buff);

    bool HasBuff(Buff buff);

    void ClearBuffs();
}
