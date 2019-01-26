using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public bool CanPickup
	{
		get { return _canPickup; }
		set { _canPickup = value; }
	}

	public ObjectGrid MyGrid;

	public float yOffset;
	private bool _canPickup = true;
}
