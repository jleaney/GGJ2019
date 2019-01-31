using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Plant : Pickup
{
    [SerializeField] public Material plant;
    private Material seedling;
	public bool isGrown;

    [SerializeField]
    private float scaleMin, scaleMax;

    [SerializeField]
    private GameObject[] grassCombos;

    public bool IsHeld { get; set; }

    private void Start()
	{
        transform.GetChild(0).gameObject.SetActive(false); // ensures fully grown state is off
        OnRelease += () => Destroy(transform.parent.gameObject);
        OnPickup += () => IsHeld = true;
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
        AudioManager.instance.CreateSFX("grow");
        SetRandomSize();

        //SetGrassCombo(); //TODO was crashing the game lol

        transform.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void PlantSeedling()
    {
		transform.GetChild(2).DOScale(transform.GetChild(2).localScale * 2.0f, 0.5f).SetEase(Ease.InOutElastic);
		transform.GetChild(2).GetChild(0).GetComponent<Animator>().SetTrigger("plant seedling");
        StartCoroutine(SeedlingParticleStart());
    }

    private IEnumerator SeedlingParticleStart()
    {
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(2).GetChild(0).GetComponent<ParticleSystem>().Play();
    }

}
