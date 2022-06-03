using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Currently handles fading between 2 tracks
    //Functionality to be expanded later.
    public AudioSource track1, track2;
    public float musicVolumeSetting = 1;
    private bool isPlayingTrack1 = true;
    private void Start()
    {
        track1.loop = true;
        track2.loop = true;
    }
    public void AudioCommand(AudioClip newClip, float duration, float targetVolume)
    {
        if (newClip = null)
        {
            FadeVolume(duration, targetVolume);
        }
        else
        {
            FadeSwap(newClip, duration, targetVolume);
        }
    }
    public void FadeVolume(float duration, float targetVolume)
    {
        if (targetVolume > 0)
        {
            targetVolume = musicVolumeSetting * targetVolume;
        }
        else
        {
            targetVolume = 0;
        }

        if (isPlayingTrack1)
        {
            StartCoroutine(FadeAudioSource.StartFade(track1, duration, targetVolume));
        }
        else
        {
            StartCoroutine(FadeAudioSource.StartFade(track2, duration, targetVolume));
        }
        
    }
    public void FadeSwap(AudioClip newClip, float duration, float targetVolume)
    {
        
        if(targetVolume > 0)
        {
            targetVolume = musicVolumeSetting * targetVolume;
        }
        else
        {
            if (isPlayingTrack1)
            {
                targetVolume = track1.volume;
            }
            else
            {
                targetVolume = track2.volume;
            }
        }
        if (isPlayingTrack1)
        {
            track2.clip = newClip;
            track2.PlayOneShot(newClip);
            track2.volume = 0;
            StartCoroutine(FadeAudioSource.StartFade(track2, duration, targetVolume));
            StartCoroutine(FadeAudioSource.StartFade(track1, duration, 0));
            isPlayingTrack1 = false;
        }
        else
        {
            track1.clip = newClip;
            track1.PlayOneShot(newClip);
            track1.volume = 0;
            StartCoroutine(FadeAudioSource.StartFade(track1, duration, targetVolume));
            StartCoroutine(FadeAudioSource.StartFade(track2, duration, 0));
            isPlayingTrack1 = true;
        }
    }
}
