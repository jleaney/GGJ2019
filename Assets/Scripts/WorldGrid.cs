using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	private const int gridSizeX = 9;
	private const int gridSizeZ = 9;

	public static WorldGrid instance;

	private void Awake()
	{
		if (instance == null) instance = this;
		else throw new Exception("Duplicate instance of WorldGrid singleton.");

		worldGrid = new GameObject[9,9];
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		for (int i = 0; i < gridSizeX; i++)
		{
			for (int j = 0; j < gridSizeZ; j++)
			{
				Gizmos.DrawWireCube(
					new Vector3(i * gridWidth, 0, j * gridWidth),
					new Vector3(gridWidth, 0, gridWidth));
			}
		}
	}

	public Vector2Int PositionToIndex(Vector2 position)
	{
		Vector2Int index = new Vector2Int();
		index.x = Mathf.RoundToInt(position.x / gridWidth);
		index.y = Mathf.RoundToInt(position.y / gridWidth);
		return index;
	}

	public bool IsValidPosition(Vector2 position)
	{
		if (position.x > gridSizeX * gridWidth || position.x < 0 || position.y > gridSizeZ * gridWidth || position.y < 0) return false;
		return true;
	}

	public Vector2 SnapToGrid(Vector3 position)
	{
		position.x = Mathf.Clamp(position.x, 0, gridSizeX * gridWidth - 0.5f*gridWidth);
		position.z = Mathf.Clamp(position.z, 0, gridSizeZ * gridWidth - 0.5f * gridWidth);
		Vector2 snappedPos;
		snappedPos.x = Mathf.Round(position.x / gridWidth) * gridWidth;
		snappedPos.y = Mathf.Round(position.z / gridWidth) * gridWidth;
		return snappedPos;
	}

	public void SetIndex(Vector2Int index, GameObject obj)
	{
		worldGrid[index.x, index.y] = obj;
	}

	public bool IsOccupied(Vector2Int index)
	{
		return worldGrid[index.x, index.y] != null;
	}

	[SerializeField] private float gridWidth = 1;
	private GameObject[,] worldGrid;
}
