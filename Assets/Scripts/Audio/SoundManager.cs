using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImportantScripts.Core.Singleton;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> SFXSetups;

    public AudioSource musicSource;
    private bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayMusicByType(MusicType.TYPE_01);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PauseAllSounds();
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            if (isPaused)
            {
                UnPauseAllSounds();
                isPaused = false;
            }
        }
    }

    public void PauseAllSounds()
    {
        // Pausa a música
        musicSource.Pause();

        // Pausa todos os efeitos sonoros
        foreach (var sfxSource in FindObjectsOfType<AudioSource>())
        {
            sfxSource.Pause();
        }
    }

    public void UnPauseAllSounds()
    {
        // Despausa a música
        musicSource.UnPause();

        // Despausa todos os efeitos sonoros
        foreach (var sfxSource in FindObjectsOfType<AudioSource>())
        {
            sfxSource.UnPause();
        }
    }

    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }

    public SFXSetup GetSFXByType(SFXType SFXType)
    {
        return SFXSetups.Find(i => i.SFXType == SFXType);
    }
}

public enum MusicType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SFXType
{
    NONE,
    SFX_TYPE_01,
    SFX_TYPE_02,
    SFX_TYPE_03
}

[System.Serializable]
public class SFXSetup
{
    public SFXType SFXType;
    public AudioClip audioClip;
}