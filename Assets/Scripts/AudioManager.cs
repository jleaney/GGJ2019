using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

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

    private Dictionary<string, AudioClip[]> sfxTypes = new Dictionary<string, AudioClip[]>();
    private Dictionary<string, AudioMixerGroup> mixers = new Dictionary<string, AudioMixerGroup>();

    private Dictionary<string, AudioClip> ambSoundsDict = new Dictionary<string, AudioClip>();

    private AudioSource music, ambience;

    [SerializeField]
    private AudioSource sfx;

    private List<AudioSource> sfxList = new List<AudioSource>();

    [SerializeField]
    private AudioMixerGroup drawerMixer, itemMixer;


	void Start () {

        sfxTypes.Add("pickup", itemPickupSFX);
        sfxTypes.Add("place", itemPlaceSFX);
        sfxTypes.Add("open", drawerOpenSFX);
        sfxTypes.Add("close", drawerCloseSFX);

        mixers.Add("open", drawerMixer);
        mixers.Add("close", drawerMixer);

        mixers.Add("place", itemMixer);
        mixers.Add("pickup", itemMixer);

        music = transform.GetChild(0).GetComponent<AudioSource>();
        ambience = transform.GetChild(1).GetComponent<AudioSource>();

        ambience.clip = ambientSounds[0];
        ambience.Play();

        music.clip = musicTracks[Random.Range(0, musicTracks.Length - 1)];
        music.Play();
	}
	
    public void CreateSFX(string type)
    {
        AudioSource newSFX = Instantiate(sfx, transform);

        newSFX.clip = sfxTypes[type][Mathf.FloorToInt(Random.Range(0, sfxTypes[type].Length))];

        newSFX.outputAudioMixerGroup = mixers[type];
        newSFX.Play();

        sfxList.Add(newSFX);
    }

    // destroys SFX after it plays
    private void LateUpdate()
    {
        foreach (AudioSource a in sfxList)
        {
            if (!a.isPlaying)
            {
                sfxList.Remove(a);
                Destroy(a);
            }
        }
    }
}
