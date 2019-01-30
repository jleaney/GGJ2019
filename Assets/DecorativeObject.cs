using DG.Tweening;

public class DecorativeObject : Pickup
{
	private void Start()
	{
		OnPlant += Expand;
        OnRelease += () => Destroy(gameObject);
    }

	private void Expand()
	{
		transform.DOScale(transform.localScale * 2.0f, 0.5f).SetEase(Ease.InOutElastic);
	}
}
