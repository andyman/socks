using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadioTuner : MonoBehaviour
{
	public Transform tuner;
	public Transform wp0;
	public Transform wp1;

	public AudioClip[] stations;
	public AudioClip wormholeSound;
	public AudioSource audioSource;

	public UnityEvent wormholeEvent;
	public AudioSource staticSound;
	public AudioSet changeAudio;

	public Vector3 targetPos;
	public void TriggerWormhole()
	{
		GetComponent<Interactable>().enabled = false;
		wormholeEvent.Invoke();

	}


	// Start is called before the first frame update
	void Start()
	{
		targetPos = tuner.position;

	}

	public void RandomPosition()
	{
		targetPos = Vector3.Lerp(wp0.position, wp1.position, Random.value);
		staticSound.pitch = Random.Range(0.8f, 1.2f);
		staticSound.volume = Random.Range(0.05f, 0.05f);
		staticSound.Stop();
		staticSound.Play();

		changeAudio.PlayRandom(transform.position, 0.3f, 0.5f, 0.8f, 1.2f);

		if (LaundrySceneController.questionAsked)
		{
			LaundrySceneController.radioTries++;

			audioSource.Stop();
			if (stations.Length > 0)
			{
				audioSource.clip = stations[Random.Range(0, stations.Length)];
			}

			if (LaundrySceneController.radioTries >= 3)
			{
				TriggerWormhole();
				audioSource.clip = wormholeSound;
			}
			audioSource.Play();

		}
		else
		{
			audioSource.Stop();
			if (stations.Length > 0)
			{
				audioSource.clip = stations[Random.Range(0, stations.Length)];
			}

			audioSource.time = audioSource.clip.length * Random.value;
			audioSource.pitch = Random.Range(0.8f, 1.2f);
			audioSource.Play();

		}

	}

	// Update is called once per frame
	void Update()
	{
		tuner.position = Vector3.Lerp(tuner.position, targetPos, Time.deltaTime * 5.0f);

	}
}
