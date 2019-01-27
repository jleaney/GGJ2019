using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public event Action OnPlant;

	public bool CanPickup
	{
		get { return _canPickup; }
		set { _canPickup = value; }
	}

	public ObjectGrid MyGrid;

	public Vector3 startPos;
	public Quaternion startRot;

	public float yOffset;
	private bool _canPickup = true;

	public void Planted()
	{
		if (OnPlant != null) OnPlant.Invoke();
		AudioManager.instance.CreateSFX("place");
	}
}
