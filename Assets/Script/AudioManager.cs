using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource music;

    private void Start()
    {
        music = GetComponent<AudioSource>();
    }

    public static void PlayMusic()
    {
        music.Play();
    }

    public static void StopMusic() 
    {
        music.Stop();
    }
}
