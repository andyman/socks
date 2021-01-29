using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioSet : ScriptableObject {
	public AudioClip[] clips;

	public AudioClip randomClip { get { return clips[Random.Range(0, clips.Length)]; } }

	public AudioSource PlayRandom(Vector3 pos, float minVolume, float maxVolume, float minPitch, float maxPitch)
	{
		return ProcAudioSource.instance.PlayOneShot(randomClip, pos, Random.Range(minVolume, maxVolume), Random.Range(minPitch, maxPitch));
	}
}
