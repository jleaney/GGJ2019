using DG.Tweening;
using UnityEngine;

public class Grass : Pickup
{
    public float minSize = 1;
    public float maxSize = 1;

    private void Start()
    {
        OnPlant += Expand;
        OnPlant += () => GetComponentInChildren<Collider>().enabled = false;
        OnRelease += () => Destroy(gameObject);
    }

    private void Expand()
    {
        transform.DOScale(transform.localScale * 2.0f * Random.Range(minSize, maxSize), 0.5f).SetEase(Ease.InOutElastic);
    }
}
