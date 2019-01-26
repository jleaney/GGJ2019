using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridTest : MonoBehaviour
{
	private void Update()
	{
		_snappedPos = FindObjectOfType<ObjectGrid>().SnapToGrid(transform.position);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(new Vector3(_snappedPos.x, 0, _snappedPos.y), 0.5f);
	}

	private Vector2 _snappedPos;
}
