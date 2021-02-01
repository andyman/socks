using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
	private List<Interactable> triggeredInteractables = new List<Interactable>();

	public RectTransform interactionMessage;
	private Camera cam;
	public Canvas myCanvas;

	public TextMeshProUGUI[] texts;

	public Color[] unpressedColor;
	public Color[] pressedColor;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		texts[0].color = unpressedColor[0];
		texts[1].color = unpressedColor[1];
		texts[2].color = unpressedColor[2];
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 pos = transform.position;
		float nearestDistance = Mathf.Infinity;
		Interactable nearestInteractable = null;

		if (triggeredInteractables.Count > 0)
		{
			// find the nearest
			for (int i = 0; i < triggeredInteractables.Count; i++)
			{
				Interactable interactable = triggeredInteractables[i];

				if (interactable != null && interactable.enabled && interactable.uses > 0)
				{
					Vector3 otherPos = interactable.transform.position;
					float dist = Vector3.Distance(otherPos, pos);
					if (dist < nearestDistance)
					{
						nearestDistance = dist;
						nearestInteractable = interactable;
					}
				}
			}

			if (nearestInteractable != null)
			{
				// show the thing over there
				Vector3 worldPos = nearestInteractable.transform.position;
				Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
				float scaleFactor = myCanvas.scaleFactor;
				Vector2 finalPosition = new Vector2(screenPoint.x / scaleFactor, screenPoint.y / scaleFactor);
				interactionMessage.anchoredPosition = finalPosition;
				interactionMessage.gameObject.SetActive(true);

				if (Input.GetButtonDown("Interact"))
				{
					nearestInteractable.Interact(this);
				}

			}
			else
			{
				interactionMessage.gameObject.SetActive(false);
			}
		}
		else
		{
			interactionMessage.gameObject.SetActive(false);
		}

		if (Input.GetButtonDown("Interact"))
		{
			texts[0].color = pressedColor[0];
			texts[1].color = pressedColor[1];
			texts[2].color = pressedColor[2];
		}

		if (Input.GetButtonUp("Interact"))
		{
			texts[0].color = unpressedColor[0];
			texts[1].color = unpressedColor[1];
			texts[2].color = unpressedColor[2];
		}
	}

	private void OnTriggerStay(Collider other)
	{
		OnTriggerEnter(other);
	}
	private void OnTriggerEnter(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();
		if (interactable != null && !triggeredInteractables.Contains(interactable))
		{
			if (interactable.triggerOnEnter)
			{
				interactable.Interact(this);
			}
			else if (interactable.uses > 0)
			{
				triggeredInteractables.Add(interactable);
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();
		if (interactable != null && triggeredInteractables.Contains(interactable))
		{

			triggeredInteractables.Remove(interactable);
		}
	}
}
