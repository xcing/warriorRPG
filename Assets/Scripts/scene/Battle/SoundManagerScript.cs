using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {

	// Use this for initialization
    public AudioSource[] sounds;
    public AudioSource atk1;
    public AudioSource atk2;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
	}

    public void PlayEffect()
    {
        sounds = this.GetComponents<AudioSource>();
        atk1 = sounds[0];
        atk2 = sounds[1];
        atk1.clip = Resources.Load("SoundEffect/slash") as AudioClip;
        atk2.clip = Resources.Load("SoundEffect/slash2") as AudioClip;

        atk1.Play();
    }
}
