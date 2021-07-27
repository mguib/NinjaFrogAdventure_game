using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    private AudioSource ausioSource;

    public AudioClip jump;
    public AudioClip coinSound;
    public AudioClip caixa;


    // Start is called before the first frame update
    void Start()
    {
        ausioSource = GetComponent<AudioSource>();
    }

    
    public void PlaySFX(AudioClip sfx)
    {
        ausioSource.PlayOneShot(sfx);
    }
}
