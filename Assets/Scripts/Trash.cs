using UnityEngine;

public class Trash : MonoBehaviour
{
	public GameObject key;
	public ParticleSystem ps;

	private void Start()
	{
		transform.localScale *= Random.Range(1f, 1.2f);
	}

	private void OnClicked()
	{
		GameManager.instance.TrashRemaining--;
		if (GameManager.instance.TrashRemaining <= 0)
		{
			Instantiate(key, new Vector3(4.5f, 0.5f, 4.5f), Quaternion.identity);
		}

		Instantiate(ps, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
