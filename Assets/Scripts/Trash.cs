using UnityEngine;

public class Trash : MonoBehaviour
{
	public GameObject key;
	private void Start()
	{
		transform.localScale *= Random.Range(1f, 1.2f);
	}

	private void OnClicked()
	{
		GameManager.instance.TrashRemaining--;
		if (GameManager.instance.TrashRemaining <= 0)
		{
			Instantiate(key, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}
}
