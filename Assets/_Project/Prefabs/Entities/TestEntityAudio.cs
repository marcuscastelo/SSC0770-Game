using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Entities;
using Hypnos.Audio;

using Zenject;

public class TestEntityAudio : IEntityAudio<Entity>
{
    private AudioSystem _audioSystem;
    
    public TestEntityAudio(AudioSystem audioSystem)
    {
        _audioSystem = audioSystem;
    }

    public void PlayAttackSound(Entity entity) => _audioSystem.PlaySFX(AudioType.SFX_Hypnos_Attack_Hand);
    public void PlayDeathSound(Entity entity) => _audioSystem.PlaySFX(AudioType.SFX_Hypnos_Attack_Sword);
    public void PlayWalkSound(Entity entity) => _audioSystem.PlaySFX(AudioType.SFX_Hypnos_Hurt);
    public void PlayDashSound(Entity entity) => _audioSystem.PlaySFX(AudioType.SFX_Hypnos_Attack_Sword_Miss);
}
