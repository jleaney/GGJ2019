using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedlingEvent : MonoBehaviour {

    [SerializeField]
    private Plant plant;

    public void CallGrow()
    {
        GetComponentInParent<Plant>().CompleteGrowing();
    }
}
