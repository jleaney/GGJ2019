using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{
	private const int maxX = 3;
	private const int maxZ = 3;
	private const int minX = -4;
	private const int minZ = -4;

	public static World instance;
	public static Tilemap tilemap;

	public Tile newTile;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			tilemap.SetTile(ClampToMap(new Vector3Int(0, -21, 0)), newTile);
		}
	}

	private void Awake()
	{
		if (instance == null) instance = this;
		else throw new Exception("Duplicate instance of World singleton.");
		tilemap = GetComponentInChildren<Tilemap>();
	}

	/// <summary>
	/// Clamps the Vector3 position to a value within the map
	/// </summary>
	public Vector3Int ClampToMap(Vector3Int pos)
	{
		pos.Clamp(new Vector3Int(minX, minZ, 0), new Vector3Int(maxX, maxZ, 0));
		return pos;
	}
}
