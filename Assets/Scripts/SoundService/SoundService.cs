
using System;
using UnityEngine;

public class SoundService
{
    private AudioSource bgAudioSource;
    private AudioSource sFXAudioSource;
    private AudioSource specialAudioSource;
    private SoundType[] soundTypes;

    public SoundService(AudioSource bgAudioSource, AudioSource sFXAudioSource, AudioSource specialAudioSource, SoundType[] soundTypes)
    {
        this.bgAudioSource = bgAudioSource;
        this.sFXAudioSource = sFXAudioSource;
        this.specialAudioSource = specialAudioSource;
        this.soundTypes = soundTypes;
    }

    private AudioClip GetAudioClip(Sound soundName)
    {
        SoundType item = Array.Find(soundTypes, i => i.soundName == soundName);
        if (item == null)
        {
            return null;
        }
        return item.soundClip;
    }

    public void PlaySFX(Sound soundName)
    {
        AudioClip clip = GetAudioClip(soundName);
        if (clip != null)
        {
            sFXAudioSource.PlayOneShot(clip);
        }
    }

    public void PlayBackGroundAudio(Sound soundName)
    {
        AudioClip clip = GetAudioClip(soundName);
        if (clip != null)
        {
            bgAudioSource.clip = clip;
            bgAudioSource.Play();
        }
    }

    public void PlaySpecialSound(Sound soundName)
    {
        AudioClip clip = GetAudioClip(soundName);
        if (clip != null)
        {
            specialAudioSource.clip = clip;
            specialAudioSource.Play();
        }
    }

    public void StopSpecialSound()
    {
        specialAudioSource.Stop();
        specialAudioSource.clip = null;
    }

    public void SetPauseSpecialAudioSource(bool pauseStatus)
    {
        if(pauseStatus)
        {
            specialAudioSource.Pause();
        }
        else
        {
            specialAudioSource.Play();
        }
    }

}
