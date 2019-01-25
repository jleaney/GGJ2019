using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	public static WorldGrid instance;

	private void Awake()
	{
		if (instance == null) instance = this;
		else throw new Exception("Duplicate instance of WorldGrid singleton.");
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		for (int i = -9; i < 9; i++)
		{
			for (int j = -9; j < 9; j++)
			{
				Gizmos.DrawWireCube(
					new Vector3(i * gridWidth, 0, j * gridWidth),
					new Vector3(gridWidth, 0, gridWidth));
			}
		}
	}

	public static Vector3 SnapToGrid(Vector3 position)
	{

	}

	[SerializeField] private float gridWidth = 1;
}
