using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioClip currentClip;
    public AudioSource src;
    int index;

    private void Start()
    {
        currentClip = clips[0];
    }

    public void NextTrack()
    {
        index++;
        index %= clips.Count;
        currentClip = clips[index];
        src.Stop();
        src.clip = currentClip;
    }
}
