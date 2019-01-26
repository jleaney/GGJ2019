using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
	private void Update()
	{
		Ray ray = new Ray(transform.position, Vector3.up);
		Debug.DrawRay(transform.position, Vector3.up, Color.magenta);
		RaycastHit[] hits = Physics.SphereCastAll(ray, 0.35f, 1);
		if (hits != null)
		{
			foreach (var hit in hits)
			{
				print(hit.collider.name);
				if (hit.collider.GetComponentInParent<Plant>())
				{
					hit.collider.GetComponentInParent<Plant>().transform.GetChild(2).GetComponent<Animator>().SetTrigger("complete");
				}
			}
		}
	}
}
