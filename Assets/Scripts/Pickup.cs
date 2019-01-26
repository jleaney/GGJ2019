using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public bool CanPickup { get; set; }
	public virtual bool Held { get; set; }

	public float yOffset;
}
