using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid : MonoBehaviour
{
	void Awake()
	{
		worldGrid = new GameObject[9, 9];
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		for (int i = 0; i < _gridSizeX; i++)
		{
			for (int j = 0; j < _gridSizeZ; j++)
			{
				Gizmos.DrawWireCube(
					new Vector3(transform.position.x + (i * gridWidth), transform.position.y, transform.position.z + (j * gridWidth)),
					new Vector3(gridWidth, 0, gridWidth));
			}
		}
	}

	public Vector2Int PositionToIndex(Vector3 position)
	{
		position = new Vector3(position.x - transform.position.x, transform.position.y, position.z - transform.position.z);

		Vector2Int index = new Vector2Int
		{
			x = Mathf.RoundToInt(position.x / gridWidth),
			y = Mathf.RoundToInt(position.z / gridWidth)
		};
		return index;
	}

	public Vector3 SnapToGrid(Vector3 position)
	{
		position = new Vector3(position.x - transform.position.x, transform.position.y, position.z - transform.position.z);

		position.x = Mathf.Clamp(position.x, transform.position.y, _gridSizeX * gridWidth - 0.5f * gridWidth);
		position.z = Mathf.Clamp(position.z, transform.position.y, _gridSizeZ * gridWidth - 0.5f * gridWidth);
		Vector3 snappedPos;
		snappedPos.x = Mathf.Round(position.x / gridWidth) * gridWidth;
		snappedPos.y = transform.position.y;
		snappedPos.z = Mathf.Round(position.z / gridWidth) * gridWidth;

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
	[SerializeField] private int _gridSizeX = 9;
	[SerializeField] private int _gridSizeZ = 9;
}
