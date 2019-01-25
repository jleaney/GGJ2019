using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
	public static Overlay instance;

	private void Awake()
	{
		if (instance == null) instance = this;
		else throw new Exception("Duplicate instance of Overlay singleton.");
	}

	public void FadeOut(float duration)
	{
		_overlay.DOFade(0.0f, duration);
	}

	public void FadeIn(float duration)
	{
		FadeIn(duration, Color.white);
	}

	public void FadeIn(float duration, Color color)
	{
		_overlay.color = color;
		_overlay.DOFade(1.0f, duration);
	}

	[SerializeField] private Image _overlay;
}
