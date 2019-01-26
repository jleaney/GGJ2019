using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{public void SetParticle(string effect)
	{
		foreach (Transform child in transform)
		{
			if(child.name == effect) child.GetComponent<ParticleSystem>().Play(true);
			else child.GetComponent<ParticleSystem>().Stop(true);
		}
	}
}
