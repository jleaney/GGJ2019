using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] ambientSounds, musicTracks;
    private AudioClip defaultAmbience;

    private Dictionary<string, AudioClip> ambSoundsDict = new Dictionary<string, AudioClip>();

    private AudioSource music, ambience;

	void Start () {

        music = transform.GetChild(0).GetComponent<AudioSource>();
        ambience = transform.GetChild(1).GetComponent<AudioSource>();

        ambience.clip = ambientSounds[0];
        ambience.Play();

        music.clip = musicTracks[Random.Range(0, musicTracks.Length - 1)];
        music.Play();
	}
	
	void Update () {
		
	}
}
