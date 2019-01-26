using UnityEngine;

public class Plant : Pickup
{
    [SerializeField]
    private Material plant;
    private Material seedling;
	public bool isGrown;

    [SerializeField]
    private float scaleMin, scaleMax;

    [SerializeField]
    private GameObject[] grassCombos;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false); // ensures fully grown state is off
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlantSeedling();
        }
    }

    private void SetRandomSize()
    {
        float size = Random.Range(scaleMin, scaleMax);	
        transform.localScale = new Vector3(size, size, size);
    }

    private void SetGrassCombo()
    {
        var obj = Instantiate(grassCombos[Mathf.FloorToInt(Random.Range(0, grassCombos.Length))], transform.position, transform.rotation);
		obj.transform.SetParent(transform);
    }

    public void Grow()
    {
        GetComponentInChildren<Animator>().SetTrigger("grow");
    }

    public void CompleteGrowing()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetComponentInChildren<Renderer>().material = plant;

        SetRandomSize();

        SetGrassCombo();



        transform.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void PlantSeedling()
    {
        transform.GetChild(2).GetChild(0).GetComponent<Animator>().SetTrigger("plant seedling");
    }
}
