using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class KeyController : MonoBehaviour {

    [SerializeField]
    private LockController lockController;

	private AudioSource audioSource;
	public AudioClip audioClip;

    [SerializeField]
    private AudioMixer musicMixer;

    private float currentVol;
    private float tParam = 0;

    private bool music2IsPlaying = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (tParam < 1)
        {
            tParam += Time.deltaTime * 1f;
            musicMixer.SetFloat("musicVol", Mathf.Lerp(-14, -80, tParam));
        }

        if (tParam == 1 && !music2IsPlaying)
        {
            music2IsPlaying = true;
            AudioManager.instance.transform.GetChild(0).GetComponent<AudioSource>().Stop();
            
        }
    }

    private void OnMouseDown()
	{
        audioSource.bypassEffects = true;
        audioSource.PlayOneShot(audioClip);
       
		var locks = FindObjectsOfType<LockController>();
		foreach (var l in locks)
			l.Unlock();
		GetComponent<Animator>().SetTrigger("get key");

        AudioManager.instance.transform.GetChild(0).GetComponent<AudioSource>().clip = AudioManager.instance.MusicTracks[1];
        musicMixer.SetFloat("musicVol", -14);
        AudioManager.instance.transform.GetChild(0).GetComponent<AudioSource>().Play();
    }

    public void PlayParticleFX()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }
}
