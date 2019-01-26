using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
	private void Start()
	{
		transform.localScale *= Random.Range(1f, 1.2f);
	}

	private void OnClicked()
	{
		GameManager.instance.TrashRemaining--;
		Destroy(gameObject);
	}
}
