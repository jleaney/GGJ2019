using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
				_gridPos = WorldGrid.instance.SnapToGrid(pos);

				if (!WorldGrid.instance.IsOccupied(WorldGrid.instance.PositionToIndex(_gridPos)))
				{
					print("position occupied");
					_targetPos = new Vector3(_gridPos.x + _offset.x, _heldItem.yOffset, _gridPos.y + _offset.z);
				}

				_heldItem.transform.position =
					Vector3.Lerp(_heldItem.transform.position, _targetPos, Time.smoothDeltaTime * 7.5f);

				_tileHighlighter.SetActive(true);
				_tileHighlighter.transform.position =
					new Vector3(_targetPos.x, _tileHighlighter.transform.position.y, _targetPos.z);
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
					_startPos = _heldItem.transform.position;
					_offset = hitInfo.point - _startPos;
					_heldItem.Held = true;

					_targetPos = _heldItem.transform.position;
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (_heldItem == null) return;

			_heldItem.Held = false;
			var index = WorldGrid.instance.PositionToIndex(
				new Vector2(_heldItem.transform.position.x, _heldItem.transform.position.z));

			WorldGrid.instance.SetIndex(index, _heldItem.gameObject);
			_heldItem = null;
			_tileHighlighter.SetActive(false);
		}
	}


	[SerializeField] private GameObject _tileHighlighter;

	private Pickup _heldItem;
	private Vector3 _startPos;
	private Vector3 _offset;

	private Vector3 _targetPos;
	private Vector3 _gridPos;
}
