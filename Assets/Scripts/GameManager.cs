using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public MeshRenderer grassImage;

	int startingClutterAmt = 10;
	private ObjectGrid grid;

	public List<GameObject> spawnItems;

	private int trashRemaining;
	public int TrashRemaining
	{
		get { return trashRemaining;}
		set
		{
			trashRemaining = value;
			if (trashRemaining <= 0)
			{
				DOTween.To(() => grassImage.material.GetFloat("_Blend"),
					x => grassImage.material.SetFloat("_Blend", x), 1.0f, 2.5f);
			}
		} }


	private void Awake()
	{
		if (instance == null) instance = this;
		else throw new Exception("Duplicate instance of GameManager singleton.");
	}

	void Start()
	{
		Overlay.instance.FadeIn(0);
		Overlay.instance.FadeOut(5.0f);
		PopulateWorld();
	}

	private void PopulateWorld()
	{
		grid = FindObjectOfType<ObjectGrid>();

		for (int i = 0; i < startingClutterAmt; i++)
		{
			var item = spawnItems[Random.Range(0, spawnItems.Count)];
			var x = GetEmptyIndex();
			var pos = new Vector3(x.x * grid.gridWidth, grid.transform.position.y, x.y * grid.gridWidth);
			grid.worldGrid[x.x, x.y] = Instantiate(item, pos, Quaternion.Euler(0, 45, 0));
			TrashRemaining++;
		}
	}

	private Vector2Int GetEmptyIndex()
	{
		Vector2Int index;
		do
		{
			index = new Vector2Int(
				Random.Range(0, grid.worldGrid.GetLength(0)),
				Random.Range(0, grid.worldGrid.GetLength(1)));
		} while (grid.worldGrid[index.x, index.y] != null);

		return index;
	}
}
