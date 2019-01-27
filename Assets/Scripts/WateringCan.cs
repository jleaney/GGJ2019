using DG.Tweening;
using UnityEngine;

public class WateringCan : Tool
{
	public GameObject waterIndicator;
	private Vector3 startPosition;
	private AudioSource audioSource;

	[SerializeField] private AudioClip startClip;
	[SerializeField] private AudioClip endClip;
	[SerializeField] private AudioClip loopClip;


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		startPosition = transform.position;
	}

	private void OnMouseDown()
	{
		Vector3 v3 = Input.mousePosition;
		v3 = CameraManager.instance.camera.ScreenToWorldPoint(v3);
		Ray ray = new Ray(v3, CameraManager.instance.camera.transform.forward);
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		float enter;
		if (plane.Raycast(ray, out enter))
		{
			Vector3 pos = ray.GetPoint(enter);
			_offset = pos - transform.position;
		}
		particles.Play(true);
		audioSource.PlayOneShot(startClip);
		Invoke("PlayAudioLoop", startClip.length);
		waterIndicator.SetActive(true);
	}

	void PlayAudioLoop()
	{
		audioSource.Play();
		audioSource.clip = loopClip;
	}

	private void OnMouseUp()
	{
		audioSource.Stop();
		audioSource.PlayOneShot(endClip);
		particles.Stop(true);
		waterIndicator.SetActive(false);
		transform.DOMove(startPosition, 0.5f);
	}

	private void OnMouseDrag()
	{
		Vector3 v3 = Input.mousePosition;
		v3 = CameraManager.instance.camera.ScreenToWorldPoint(v3);
		Ray ray = new Ray(v3, CameraManager.instance.camera.transform.forward);
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		float enter;
		if (plane.Raycast(ray, out enter))
		{
			Vector3 pos = ray.GetPoint(enter);
			transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, 2, pos.z), Time.smoothDeltaTime * 5f);
		}
		waterIndicator.transform.position = new Vector3(waterIndicator.transform.position.x, 0.025f, waterIndicator.transform.position.z);
	}

	private bool _watering;
	[SerializeField] private Vector3 _offset;
	[SerializeField] private ParticleSystem particles;
}
