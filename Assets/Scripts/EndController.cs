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

    private bool screenshotTaken = false;
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

        if (tParam >= 1 && !screenshotTaken)
        {
            TakeScreenshot();
            screenshotTaken = true;
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
    }

    private void TakeScreenshot()
    {
        // take actual screenshot
        overlayCanvas.OverlayImage.color = new Color(1, 1, 1, 0.8f);
        overlayCanvas.FadeOut(3);
    }
}
