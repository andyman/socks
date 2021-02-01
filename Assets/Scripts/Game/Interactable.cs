using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	// set to true of auto trigger when entering
	// otherwise it will trigger when the player hits the interaction button
	public bool triggerOnEnter = false;
	public int uses = 10000;

	public UnityEvent interactionEvent;

	public void Interact(Interactor interactor)
	{
		if (uses <= 0)
		{
			return;
		}

		uses--;
		interactionEvent.Invoke();
	}

}
