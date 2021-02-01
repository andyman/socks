using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPositionFollowWorld : MonoBehaviour
{
	public Transform target;
	private Camera cam;
	private Canvas myCanvas;
	private RectTransform myRectTransform;
	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		myCanvas = GetComponentInParent<Canvas>();
		myRectTransform = GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 worldPos = target.position;
		Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
		float scaleFactor = myCanvas.scaleFactor;
		Vector2 finalPosition = new Vector2(screenPoint.x / scaleFactor, screenPoint.y / scaleFactor);
		myRectTransform.anchoredPosition = finalPosition;
	}
	void LateUpdate()
	{
		Update();
	}
}
