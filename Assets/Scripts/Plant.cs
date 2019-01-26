using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Pickup
{
    [SerializeField]
    private Material plant;

    [SerializeField]
    private float scaleMin, scaleMax;

    [SerializeField]
    private GameObject[] grassCombos;
    private void Start()
    {
        // set seeding
    }

    private void SetRandomSize()
    {
        float size = Random.Range(scaleMin, scaleMax);
        transform.localScale = new Vector3(size, size, size);
    }

    private void SetGrassCombo()
    {
        Instantiate(grassCombos[Mathf.FloorToInt(Random.Range(0, grassCombos.Length))], transform.parent);
    }

    private void Grow()
    {
        // seedling grow animation
    }

    public void CompleteGrowing()
    {
        transform.GetComponentInChildren<Renderer>().material = plant;

        SetRandomSize();

        SetGrassCombo();
    }
}
