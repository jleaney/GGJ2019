using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

public class WeatherManager : MonoBehaviour
{
	public List<WeatherPreset> presets;
	private WeatherPreset preset;
	public PostProcessVolume postProcessing;
	public ParticleManager particleManager;
	private int currentPreset;

    [SerializeField]


	private void Start()
	{
		SetWeather(presets[0]);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			currentPreset++;
			if (currentPreset >= presets.Count) currentPreset = 0;
			preset = presets[currentPreset];
			SetWeather(preset);
		}
	}

	public void SetWeather(WeatherPreset preset)
	{
		DOTween.To(() => RenderSettings.skybox.GetColor("_TintColor"),
			x => RenderSettings.skybox.SetColor("_TintColor", x),
			preset.skyboxTint, 0.5f);

		DOTween.To(() => RenderSettings.skybox.GetFloat("_Exposure"),
			x => RenderSettings.skybox.SetFloat("_Exposure", x),
			preset.exposure, 0.5f);

		postProcessing.profile = preset.postProcessProfile;
		particleManager.SetParticle(preset.particleEffect);
        GetComponent<AudioSource>().clip = preset.sound;
        GetComponent<AudioSource>().Play();
	}

	private void OnGUI()
	{
		if (preset != null) GUILayout.Label(preset.postProcessProfile.name);
	}
}

[Serializable]
public class WeatherPreset
{
	public PostProcessProfile postProcessProfile;
	public Color skyboxTint;
	public float exposure;
	public string particleEffect;
	public Sprite uiButton;
    public AudioClip sound;
}
