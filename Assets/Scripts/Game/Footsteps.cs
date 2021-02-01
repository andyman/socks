using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
	private Vector3 lastFootstepPosition;
	private float nextFootstepTime;

	//public string footstepPSName = "FootstepParticleSystem";

	//ParticleSystem footstepPS = null;

	//public Color psColor;

	public AudioSet stepSounds;

	// Start is called before the first frame update
	void Start()
	{
		//footstepPS = Cached.Find(footstepPSName).GetComponent<ParticleSystem>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (Time.time >= nextFootstepTime)
		{
			Vector3 pos = transform.position;
			//pos.y = 0.1f;
			//if (Vector3.Distance(pos, lastFootstepPosition) > 0.1f)
			{
				nextFootstepTime = Time.time + 0.1f;
				lastFootstepPosition = pos;
				MakeFootstep(pos);
			}

		}
	}

	private void MakeFootstep(Vector3 pos)
	{
		//Debug.Log("make footstep");
		//footstepPS.transform.position = pos;
		//footstepPS.startColor = psColor;
		//footstepPS.Emit(1);
		stepSounds.PlayRandom(transform.position, 0.01f, 0.05f, 0.8f, 1.2f);
	}
}
