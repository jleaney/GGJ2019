using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager instance;
	public Camera camera;

	private void Awake()
	{
		if (instance == null) instance = this;
		else throw new Exception("Duplicate instance of CameraManager singleton.");
		camera = GetComponentInChildren<Camera>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
			transform.Rotate(transform.up, -90);

		if (Input.GetKeyDown(KeyCode.E))
			transform.Rotate(transform.up, 90);
	}
}
