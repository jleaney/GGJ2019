using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("Multiple Tooltips in scene.");
    }

    private void Update()
    {
        Vector3 v3 = Input.mousePosition;
        v3 = CameraManager.instance.camera.ScreenToWorldPoint(v3);

        Ray ray = new Ray(v3, CameraManager.instance.camera.transform.forward);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.GetComponentInParent<Plant>())
            {
                if (hitInfo.collider.GetComponentInParent<Plant>().CanPickup)
                {
                    if (!hitInfo.collider.GetComponentInParent<Plant>().IsHeld)
                    {
                        var tex = (Texture2D)hitInfo.collider.GetComponentInParent<Plant>().plant.mainTexture;
                        _tooltipImage.sprite =
                            Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1.5f,
                            Time.smoothDeltaTime * 12.5f);
                        transform.position = hitInfo.transform.position + new Vector3(0.0f, 0.55f, -0.2f);
                    }
                    else
                        Shrink();
                }
                else
                    Shrink();
                
            }
            else
                Shrink();
        }
        else
            Shrink();
    }

    private void Shrink()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.smoothDeltaTime * 15);
    }

    [SerializeField] private Image _tooltipImage;
}
