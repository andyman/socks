using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterExitScript : MonoBehaviour
{
	public int uses = 1;
	public UnityEvent enterEvent;
	public UnityEvent exitEvent;

	private void OnTriggerEnter(Collider other)
	{
		if (uses <= 0) return;
		uses--;

		enterEvent.Invoke();
	}
	private void OnTriggerExit(Collider other)
	{
		if (uses <= 0) return;
		uses--;

		exitEvent.Invoke();
	}
}
