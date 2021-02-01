using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHider : MonoBehaviour
{
	private float mouseVisibleUntilTime;
	private Vector3 lastMousePosition;

	// Start is called before the first frame update
	void Start()
	{
		mouseVisibleUntilTime = Time.time + 1.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time > mouseVisibleUntilTime)
		{
			Cursor.visible = false;
		}

		Vector3 newMousePosition = Input.mousePosition;
		if (newMousePosition != lastMousePosition)
		{
			mouseVisibleUntilTime = Time.time + 1.0f;
			Cursor.visible = true;
		}
		lastMousePosition = newMousePosition;
	}
}
