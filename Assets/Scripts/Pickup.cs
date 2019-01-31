using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public event Action OnPlant;
	public event Action OnRelease;
    public event Action OnPickup;

    public bool CanPickup
	{
		get { return _canPickup; }
		set { _canPickup = value; }
	}


    public ObjectGrid MyGrid;

	public Vector3 startPos;
	public Vector3 targetPos;
    public Vector3 startSize;
    public Quaternion startRot;

	public float yOffset;
	private bool _canPickup = true;

    public void PickedUp()
    {
        if (OnPickup != null) OnPickup.Invoke();
        ResetScale();
    }

    public void Planted()
	{
		if (OnPlant != null) OnPlant.Invoke();
		AudioManager.instance.CreateSFX("place");
	}

    public void ResetScale()
    {
        transform.DOScale(transform.localScale / 2f, 0.5f);
    }

    public void Remove()
    {
        if (OnRelease != null) OnRelease.Invoke();
    }
}
