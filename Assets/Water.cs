using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.GetComponentInParent<Plant>()) return;
		if (other.gameObject.GetComponentInParent<Plant>().isGrown) return;
		other.gameObject.GetComponentInParent<Plant>().transform.GetChild(2).GetChild(0).GetComponent<Animator>().SetTrigger("complete");
		other.gameObject.GetComponentInParent<Plant>().isGrown = true;
	}
}
