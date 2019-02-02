using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using UnityEngine.Audio;
using DG.Tweening;
using TMPro;

public class EndController : MonoBehaviour
{

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

    public TMP_InputField nameInput;

    [SerializeField]
    private Overlay overlayCanvas;

    [SerializeField]
    private bool takeScreenshotAtEnd = false;

    [SerializeField]
    private float screenshotFadeTime = 3;

    private bool screenshotTaken = false;
    private bool fadeOutStarted = false;

    public Button weatherButton;
    public Button shareButton;

    [SerializeField]
    private GameObject endMenu;

    [SerializeField]
    private GameObject wateringCan;

    public AudioMixer musicMixer;
    void Start()
    {
        nameInput.gameObject.SetActive(false);
        weatherButton.onClick.AddListener(NextPreset);
        postProcessing.profile.TryGetSettings(out vignetteLayer); // sets up the vignette layer / effect
        musicMixer.DOSetFloat("ambienceVol", -12, 2);
        musicMixer.DOSetFloat("musicVol", -14, 2);
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
    }

    public void NextPreset()
    {
        var sprite = FindObjectOfType<WeatherManager>().NextWeather();
        weatherButton.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }

    private bool nameDone = false;
    void Screenshot()
    {
        if (!nameDone)
        {
            nameInput.gameObject.SetActive(true);
            nameDone = true;
        }
        else
        {
            nameInput.gameObject.SetActive(false);
            shareButton.gameObject.SetActive(false);
            CaptureScreenshot();
        }
    }

    private void CaptureScreenshot()
    {
        var path = Application.persistentDataPath + $"/terrarium{DateTime.Now:yyMMddhhmmss}.png";

        overlayCanvas.gameObject.SetActive(false);
        wateringCan.SetActive(false);
        ScreenCapture.CaptureScreenshot(path);

        string message = nameInput.text;
        StartCoroutine(TweetWhenFinished(path, message));
    }

    private IEnumerator TweetWhenFinished(string path, string message)
    {
        float timer = 10;
        while (!File.Exists(path))
        {
            timer -= Time.deltaTime;
            if (timer <= 0) yield break;
            yield return null;
        }
        
        const string ConsumerKey = "icdsL2UA6daJcvuZNNoXY4Zkc",
            ConsumerKeySecret = "FAi9LwSWJYPZSw5ewwIAKjvhfjFeO0wDbTax4KKPGzV5Ns7BKY",
            AccessToken = "1089499928781414401-sien8RJ3ybIQJTpDKlAHkfNd1aGflu",
            AccessTokenSecret = "ztqnkydk9fxnHK9GZHEulJJcqruBiMouCpgLydRqZeXCC";

        var twitter = new Twitter(
            ConsumerKey,
            ConsumerKeySecret,
            AccessToken,
            AccessTokenSecret
        );

        // 1) if we want to upload image and post it together with text

        overlayCanvas.gameObject.SetActive(true);
        wateringCan.SetActive(true);
        string response = twitter.PublishToTwitter($"{message} #MyTerrarium", path);
        Console.WriteLine(response);
    }

    public void TriggerEnd()
    {
        dome.GetComponent<Animator>().SetTrigger("close roof");
        increaseVignette = true;
        wateringCan.SetActive(false);
        overlayCanvas.GameEnded = true;
    }

    public void FadeMusic()
    {
        musicMixer.DOSetFloat("ambienceVol", -80, 2);
        musicMixer.DOSetFloat("musicVol", -80, 2);
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

        endMenu.GetComponent<Animator>().SetTrigger("open");
    }
}


