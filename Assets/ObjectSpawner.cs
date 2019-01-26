using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	public GameObject prefab;

	private Pickup spawn;

	private void Start()
	{
		SpawnNew();
	}

	public void SpawnNew()
	{
		spawn = Instantiate(prefab, transform.position, prefab.transform.rotation).GetComponentInChildren<Pickup>();
		spawn.transform.parent.SetParent(transform.parent);
		spawn.transform.localScale *= 0.5f;
		spawn.OnPlant += SpawnNew;
	}
}
