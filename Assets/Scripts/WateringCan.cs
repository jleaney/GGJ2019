using UnityEngine;

public class WateringCan : Tool
{
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
	}

	private void OnMouseUp()
	{
		particles.Play(false);
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
			transform.position = new Vector3(pos.x, transform.position.y, pos.z);
		}
	}

	private bool _watering;
	[SerializeField] private Vector3 _offset;
	[SerializeField] private ParticleSystem particles;
}
