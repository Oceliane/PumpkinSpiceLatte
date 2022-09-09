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

    public static void PlayMusic()
    {
        music.Play();
    }

    public static void StopMusic() 
    {
        music.Stop();
    }

    public void ToggleMusic(bool toggle)
    {
        isMusic = toggle;
    }
}
