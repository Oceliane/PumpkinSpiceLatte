using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource music;
    [HideInInspector] public bool isMusic;

    private void Start()
    {
        music = GetComponent<AudioSource>();
        isMusic = true;
    }

    public void ResolveMusic()
    {
        if (isMusic)
            music.Play();
        else if (music.isPlaying)
            music.Stop();
            
    }

    public void ToggleMusic(bool toggle)
    {
        isMusic = toggle;
    }

    public void StopMusic()
    {
        music.Stop();
    }
}
