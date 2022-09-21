using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static AudioSource Play2DClip(AudioClip clip, float volume)
    {
        GameObject audioObject = new GameObject("Audio2D");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();
        Object.Destroy(audioObject, clip.length);

        return audioSource;
    }
}
