using UnityEngine;
using System.Collections;

public class SoundSingleton : MonoBehaviour {

    public static SoundSingleton Instance;
    private AudioSource ButtonPressed;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            ButtonPressed = this.GetComponent<AudioSource>();
        }
        else
            Destroy(this.gameObject);
    }


    public void PlaySound()
    {
        if(!ButtonPressed.isPlaying)
            ButtonPressed.PlayOneShot(ButtonPressed.clip, PlayerPrefs.GetFloat("Volume") / 100);
    }
}
