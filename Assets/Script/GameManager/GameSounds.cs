using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour
{

    public static GameSounds Instance { get; private set; }

    public bool MusicOn = true;

    public bool SFXOn = true;

    [SerializeField]
    private AudioSource Music;

    [SerializeField]
    private AudioSource SFX;



    private enum EffectSound { Itens, PassosRei, PassosEquipe } // lista de sfx

    private enum MusicSound { music1, music2, music3 } // lista de music

    [SerializeField]
    private List<AudioClip> EFsounds = new List<AudioClip>(); //lista do audioclips referente as efSound



    [SerializeField]
    private List<AudioClip> Msounds = new List<AudioClip>(); //lista do audioclips referente as efSound
   
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance == this)
        {
            Destroy(gameObject);  
        }
    }


 

    public void CheckButtonSFX()
    {
        if (SFXOn)
        {
            SFXOn = false;


        }
        else
        {
            SFXOn = true;


        }
    }

    public void CheckButtonMusic()
    {
        if (MusicOn)
        {
            MusicOn = false;


        }
        else
        {
            MusicOn = true;



        }
    }

    private void PlaySFX(AudioClip sound)
    {
        if (SFXOn)
            SFX.PlayOneShot(sound);
    }
    private void PlayMusic(AudioClip sound)
    {
        if (MusicOn)
        {
            Music.clip = sound;
            Music.Play();
        }
    }

    public void PlaySfx3() // sfx especifico
    {

       PlaySFX(EFsounds[(int)EffectSound.PassosEquipe]);
    }

    public void PlaySfx2() // sfx especifico
    {

        PlaySFX(EFsounds[(int)EffectSound.PassosRei]);
    }

    public void PlaySfx1() // sfx especifico
    {

        PlaySFX(EFsounds[(int)EffectSound.Itens]);
    }

    public void PlayMusic1() // musica especifica
    {

        PlayMusic(Msounds[(int)MusicSound.music1]);
    }
    public void PlayMusic2() // musica especifica
    {

        PlayMusic(Msounds[(int)MusicSound.music2]);
    }
}
