﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

    [SerializeField]
    private float pitchVariation;

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(gameObject);
	}

	[SerializeField]
	private AudioClip[] ambientSounds, musicTracks;
	private AudioClip defaultAmbience;

	// Arrays of SFX for interactions
	[SerializeField]
	private AudioClip[] itemPickupSFX;
	[SerializeField]
	private AudioClip[] itemPlaceSFX;
	[SerializeField]
	private AudioClip[] drawerOpenSFX;
	[SerializeField]
	private AudioClip[] drawerCloseSFX;
	[SerializeField]
	private AudioClip[] growSFX;
	[SerializeField]
	private AudioClip[] digSFX;

	private Dictionary<string, AudioClip[]> sfxTypes = new Dictionary<string, AudioClip[]>();
	private Dictionary<string, AudioMixerGroup> mixers = new Dictionary<string, AudioMixerGroup>();

	private Dictionary<string, AudioClip> ambSoundsDict = new Dictionary<string, AudioClip>();

	private AudioSource music, ambience;

	[SerializeField]
	private AudioSource sfx;

	private List<AudioSource> sfxList = new List<AudioSource>();

	[SerializeField]
	private AudioMixerGroup drawerMixer, itemMixer, growMixer, digMixer;

    public AudioClip[] MusicTracks
    {
        get
        {
            return musicTracks;
        }

        set
        {
            musicTracks = value;
        }
    }

    void Start()
	{
		sfxTypes.Add("grow", growSFX);
		sfxTypes.Add("dig", digSFX);
		sfxTypes.Add("pickup", itemPickupSFX);
		sfxTypes.Add("place", itemPlaceSFX);
		sfxTypes.Add("open", drawerOpenSFX);
		sfxTypes.Add("close", drawerCloseSFX);

		mixers.Add("open", drawerMixer);
		mixers.Add("close", drawerMixer);

		mixers.Add("place", itemMixer);
		mixers.Add("pickup", itemMixer);

		mixers.Add("dig", digMixer);
		mixers.Add("grow", growMixer);

		music = transform.GetChild(0).GetComponent<AudioSource>();

		music.clip = musicTracks[Random.Range(0, musicTracks.Length - 1)];
		music.Play();
	}

	public AudioSource CreateSFX(string type, bool looping, bool randomizePitch)
	{
		AudioSource newSFX = Instantiate(sfx, transform);

		newSFX.clip = sfxTypes[type][Mathf.FloorToInt(Random.Range(0, sfxTypes[type].Length))];

		newSFX.loop = looping;
		newSFX.outputAudioMixerGroup = mixers[type];
        newSFX.pitch = Random.Range(newSFX.pitch - pitchVariation, newSFX.pitch + pitchVariation);
		newSFX.Play();

		sfxList.Add(newSFX);
		return newSFX;
	}

	public void CreateSFX(string type)
	{
		CreateSFX(type, false, true);
	}

	// destroys SFX after it plays
	private void LateUpdate()
	{
		for (var i = sfxList.Count - 1; i >= 0; i--)
		{
			AudioSource a = sfxList[i];
			if (!a.isPlaying)
			{
				sfxList.Remove(a);
				Destroy(a);
			}
		}
	}
}
