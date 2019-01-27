using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EndController : MonoBehaviour {

    [SerializeField]
    private GameObject dome;

    [SerializeField]
    private PostProcessVolume postProcessing;

    private Vignette vignetteLayer;

    private float startIntensity;
    [SerializeField]
    private float endIntensity; // vignette final intensity

    [SerializeField]
    private float vignetteSpeed;
    private float tParam = 0;
    private float valToBeLerped;

    [SerializeField] // serialized for testing only
    private bool increaseVignette;

    [SerializeField]
    private Overlay overlayCanvas;

    [SerializeField]
    private bool takeScreenshotAtEnd = false;

    [SerializeField]
    private float screenshotFadeTime = 3;

    private bool screenshotTaken = false;
    private bool fadeOutStarted = false;

    [SerializeField]
    private GameObject endMenu;

    [SerializeField]
    private GameObject wateringCan;
	void Start () {

        postProcessing.profile.TryGetSettings(out vignetteLayer); // sets up the vignette layer / effect
    }

    private void FixedUpdate()
    {
        if (increaseVignette)
        {
            if (tParam < 1)
            {
                tParam += Time.deltaTime * vignetteSpeed;
            }

            valToBeLerped = Mathf.Lerp(startIntensity, endIntensity, tParam);

            vignetteLayer.intensity.value = valToBeLerped;
        }

        if (tParam >= 1 && !screenshotTaken && takeScreenshotAtEnd && !fadeOutStarted)
        {
            StartCoroutine(GameEndUI());
            TakeScreenshot();
            screenshotTaken = true;
        }

        else if (tParam >= 1 && !fadeOutStarted)
        {
            StartCoroutine(GameEndUI());
        }

        // TEMP
        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerEnd();
        }
    }

    public void TriggerEnd()
    {
        dome.GetComponent<Animator>().SetTrigger("close roof");
        increaseVignette = true;
        wateringCan.SetActive(false);
    }

    private void TakeScreenshot()
    {
        // take actual screenshot here
        overlayCanvas.OverlayImage.color = new Color(1, 1, 1, 0.8f); // white screenshot visual indication
        // minimize screenshot here
        overlayCanvas.FadeOut(screenshotFadeTime);
    }

    private IEnumerator GameEndUI()
    {
        fadeOutStarted = true;

        if (takeScreenshotAtEnd)
        {
            yield return new WaitForSeconds(screenshotFadeTime);
        }

        else
        {
            yield return new WaitForSeconds(0);
        }

        Color black = new Color(0, 0, 0, 0);
        overlayCanvas.FadeIn(3, black, 0.5f);

        endMenu.SetActive(true);        
    }
}


