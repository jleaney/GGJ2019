using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	private void Update()
	{
		if (_heldItem != null)
		{
			ItemFollowGrid();
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 v3 = Input.mousePosition;
			v3 = CameraManager.instance.camera.ScreenToWorldPoint(v3);

			Ray ray = new Ray(v3, CameraManager.instance.camera.transform.forward);

			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo))
			{
				hitInfo.collider.SendMessage("OnClicked", null, SendMessageOptions.DontRequireReceiver);
				if (hitInfo.collider.GetComponentInParent<Pickup>())
				{
					if (hitInfo.collider.GetComponentInParent<Pickup>().CanPickup == true)
					{
						_heldItem = hitInfo.collider.GetComponentInParent<Pickup>();
						_heldItem.transform.SetParent(null);
						_startPos = _heldItem.transform.position;
						_offset = hitInfo.point - _startPos;
						_targetPos = _heldItem.transform.position;

						if (_heldItem.MyGrid != null)
						{
							Vector3 itemPos = _heldItem.MyGrid.SnapToGrid(_heldItem.transform.position);
							Vector2Int itemIndex = _heldItem.MyGrid.PositionToIndex(itemPos);
							_heldItem.MyGrid.SetIndex(itemIndex, null);
						}
					}
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			Vector3 v3 = Input.mousePosition;
			v3 = CameraManager.instance.camera.ScreenToWorldPoint(v3);

			Ray ray = new Ray(v3, CameraManager.instance.camera.transform.forward);

			RaycastHit hitInfo;
			if (_heldItem == null) return;

			_grid = null;
			if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Grid")))
			{
				_grid = hitInfo.collider.GetComponent<ObjectGrid>();
			}
			else
			{
				var hits = Physics.RaycastAll(ray);
				if (hits.Any(x => x.collider.GetComponent<DrawerBase>()))
				{
					_heldItem.transform.SetParent(hits.First(x => x.collider.GetComponent<DrawerBase>()).transform);
					_heldItem = null;
					_tileHighlighter.SetActive(false);
				}
			}

			if (_grid == null) return;

			Vector2Int index = _grid.PositionToIndex(
				new Vector3(_heldItem.transform.position.x,_heldItem.transform.position.y ,_heldItem.transform.position.z));

			_grid.SetIndex(index, _heldItem.gameObject);
			_heldItem.CanPickup = false;
			if (_heldItem.GetComponentInParent<Plant>())
			{
				_heldItem.GetComponentInParent<Plant>().PlantSeedling();
			}
			_heldItem.transform.SetParent(null);
			_heldItem = null;
			_tileHighlighter.SetActive(false);
		}
	}

	private void ItemFollowGrid()
	{
		Vector3 v3 = Input.mousePosition;
		v3 = CameraManager.instance.camera.ScreenToWorldPoint(v3);
		Ray ray = new Ray(v3, CameraManager.instance.camera.transform.forward);

		RaycastHit hitInfo;
		_grid = null;
		if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Grid")))
		{
			_grid = hitInfo.collider.GetComponent<ObjectGrid>();
		}

		if (_grid != null)
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			float enter;
			if (plane.Raycast(ray, out enter))
			{
				Vector3 pos = ray.GetPoint(enter);
				_gridPos = _grid.SnapToGrid(pos);
				Debug.DrawRay(_gridPos, Vector3.up, Color.magenta);

				Vector2Int positionToIndex = _grid.PositionToIndex(_gridPos);
				if (!_grid.IsOccupied(positionToIndex))
				{
					_targetPos = new Vector3(_gridPos.x + _offset.x, _heldItem.yOffset, _gridPos.z + _offset.z);
					Debug.DrawRay(_targetPos, Vector3.up, Color.blue);
				}

				_heldItem.transform.position =
					Vector3.Lerp(_heldItem.transform.position, _targetPos, Time.smoothDeltaTime * 7.5f);

				_tileHighlighter.SetActive(true);
				_tileHighlighter.transform.position =
					new Vector3(_gridPos.x, _gridPos.y + 0.01f, _gridPos.z);
			}
		}
		else
		{
			if (Physics.Raycast(ray, out hitInfo))
			{
				var hits = Physics.RaycastAll(ray);
				if (hits.Any(x => x.collider.GetComponent<DrawerBase>()))
				{
					Plane plane = new Plane(Vector3.up, hits.First(x => x.collider.GetComponent<DrawerBase>()).transform.position);
					float enter;
					if (plane.Raycast(ray, out enter))
					{
						Vector3 pos = ray.GetPoint(enter);
						_heldItem.transform.position =
							Vector3.Lerp(_heldItem.transform.position, pos, Time.smoothDeltaTime * 7.5f);
					}
				}
				else
				{
					Plane plane = new Plane(Vector3.up, Vector3.zero);
					float enter;
					if (plane.Raycast(ray, out enter))
					{
						Vector3 pos = ray.GetPoint(enter);
						_heldItem.transform.position =
							Vector3.Lerp(_heldItem.transform.position, pos, Time.smoothDeltaTime * 7.5f);
					}
				}
			}
		}
	}


	[SerializeField] private GameObject _tileHighlighter;

	private Pickup _heldItem;
	private Vector3 _startPos;
	private Vector3 _offset;

	private Vector3 _targetPos;
	private Vector3 _gridPos;
	private ObjectGrid _grid;
}
