using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeechTrigger : MonoBehaviour
{
	public SpeechLine enterLine;

	public bool oneTime = false;
	Collider lastOther;
	public OneLiner speaker = null;
	public UnityEvent speakEvent;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("1");
		if (enterLine == null || enterLine.text == "")
		{
			return;
		}

		if (speaker == null)
		{
			speaker = other.attachedRigidbody.GetComponentInChildren<OneLiner>();
		}
		PlayEnter();
	}

	[ContextMenu("PlayEnter")]
	public void PlayEnter()
	{
		OneLiner liner = speaker;

		if (liner == null)
		{
			Debug.Log("No liner found");
			lastOther = null;
			return;
		}
		else
		{
			Debug.Log("play speech");
		}
		liner.Play(enterLine);

		if (oneTime)
		{
			enterLine = null;
		}
		lastOther = null;
		speakEvent.Invoke();
	}


}
