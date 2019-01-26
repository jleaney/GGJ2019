using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
	public Texture2D cursorClosed;
	public Texture2D cursorOpen;
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			UnityEngine.Cursor.SetCursor(cursorClosed, Vector2.one * 100, CursorMode.Auto);
		}

		if (Input.GetMouseButtonUp(0))
		{
			UnityEngine.Cursor.SetCursor(cursorOpen, Vector2.one * 100, CursorMode.Auto);
		}
	}
}
