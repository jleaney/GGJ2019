using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeSounds : MonoBehaviour {

    // Use this for initialization

    public AudioClip[] domeSounds;
	
    public void PlaySound(int sound)
    {
        GetComponent<AudioSource>().PlayOneShot(domeSounds[sound]);
    }
}
