using DG.Tweening;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;

    private Pickup spawn;

    public bool faceEast;

    private void Start()
    {
        SpawnNew();
    }

    public void SpawnNew()
    {
        spawn = Instantiate(prefab, transform.position, Quaternion.Euler(90, faceEast ? 0 : 90, 0)).GetComponentInChildren<Pickup>();
        spawn.startSize = spawn.transform.localScale;
        spawn.transform.localScale *= 1.25f;
        spawn.transform.parent.SetParent(transform.parent);
        spawn.OnPlant += SpawnNew;
        spawn.OnRelease += SpawnNew;
    }
}
