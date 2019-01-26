using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedlingEvent : MonoBehaviour {

	public void CallGrow()
    {
        GetComponentInParent<Plant>().CompleteGrowing();
    }

    public void ParticleSystemPlay()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
