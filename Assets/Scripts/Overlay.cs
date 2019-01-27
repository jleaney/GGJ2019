using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Overlay : MonoBehaviour
{
	public static Overlay instance;
    private bool menuOut = false;

    private bool gameEnded = false;

	private void Awake()
	{
		if (instance == null) instance = this;
		else throw new Exception("Duplicate instance of Overlay singleton.");
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnded)
        {
            if (!menuOut)
            {
                menuOut = true;
                transform.GetChild(2).GetComponent<Animator>().SetTrigger("open");
                StartCoroutine(CloseMenu());
            }

            else
            {
                menuOut = false;
                transform.GetChild(2).GetComponent<Animator>().SetTrigger("close");
            }
        }
    }
    private IEnumerator CloseMenu()
    {
        yield return new WaitForSeconds(10);

        if (menuOut)
        {
            //transform.GetChild(2).GetComponent<Animator>().SetTrigger("close");
        }
        
    }

    public void FadeOut(float duration)
	{
		_overlay.DOFade(0.0f, duration);
	}

	public void FadeIn(float duration)
	{
		FadeIn(duration, Color.white, 1);
	}

	public void FadeIn(float duration, Color color, float alpha)
	{
		_overlay.color = color;
		_overlay.DOFade(alpha, duration);
	}

	[SerializeField] private Image _overlay;

    public Image OverlayImage
    {
        get
        {
            return _overlay;
        }
    }

    public bool GameEnded
    {
        get
        {
            return gameEnded;
        }

        set
        {
            gameEnded = value;
        }
    }
}
