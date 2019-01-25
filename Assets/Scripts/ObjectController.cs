using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	private void Update()
	{
		if (_heldItem != null)
		{
			Vector3 v3 = Input.mousePosition;
			v3 = CameraManager.instance.camera.ScreenToWorldPoint(v3);
			Ray ray = new Ray(v3, CameraManager.instance.camera.transform.forward);
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			float enter;
			if (plane.Raycast(ray, out enter))
			{
				Vector3 pos = ray.GetPoint(enter);
				Vector2 snappedPos = WorldGrid.instance.SnapToGrid(pos);
				Vector3 targetPos = new Vector3(snappedPos.x, 0, snappedPos.y);
				_heldItem.transform.position =
					Vector3.Lerp(_heldItem.transform.position, targetPos, Time.smoothDeltaTime * 7.5f);
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 v3 = Input.mousePosition;
			v3 = CameraManager.instance.camera.ScreenToWorldPoint(v3);

			Ray ray = new Ray(v3, CameraManager.instance.camera.transform.forward);

			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo))
			{
				if (hitInfo.collider.GetComponentInParent<Pickup>())
				{
					_heldItem = hitInfo.collider.GetComponentInParent<Pickup>();
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			_heldItem = null;
		}
	}

	private Pickup _heldItem;
}
