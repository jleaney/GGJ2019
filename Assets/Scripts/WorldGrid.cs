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

	public Vector2 SnapToGrid(Vector3 position)
	{
		Vector2 snappedPos;
		snappedPos.x = Mathf.Round(position.x / gridWidth) * gridWidth;
		snappedPos.y = Mathf.Round(position.z / gridWidth) * gridWidth;
		return snappedPos;
	}

	[SerializeField] private float gridWidth = 1;
}
