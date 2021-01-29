using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour {

	public Renderer myRenderer;

	public Material openMaterial;
	public Material closedMaterial;
	public float minBlinkDuration = 0.05f;
	public float maxBlinkDuration = 0.2f;
	public float minBlinkInterval = 0.5f;
	public float maxBlinkInterval = 3.0f;
	public bool blinking = false;

	public float nextBlinkTime = 0.0f;
	public float nextOpenTime = 0.0f;

	private Material[] sharedMaterials;
	public int eyeMaterialIndex = 1;
	// Use this for initialization
	void Start () {
		sharedMaterials = myRenderer.sharedMaterials;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextBlinkTime)
		{
			float blinkDuration = Random.Range(minBlinkDuration, maxBlinkDuration);
			nextBlinkTime = Time.time + Random.Range(minBlinkInterval, maxBlinkInterval) + blinkDuration;
			nextOpenTime = Time.time + blinkDuration;
			sharedMaterials[eyeMaterialIndex] = closedMaterial;
			myRenderer.sharedMaterials = sharedMaterials;
			blinking = true;
		}
		if (blinking && Time.time > nextOpenTime)
		{
			blinking = false;

			sharedMaterials[eyeMaterialIndex] = openMaterial;
			myRenderer.sharedMaterials = sharedMaterials;
		}
	}

	public void ResetBlinkTime()
	{
		nextBlinkTime = Time.time;
	}
	public void ForceEyeOpen()
	{
		blinking = false;

		sharedMaterials[eyeMaterialIndex] = openMaterial;
		myRenderer.sharedMaterials = sharedMaterials;
		nextBlinkTime = Time.time + 1000.0f;
		nextOpenTime = nextBlinkTime;
	}
	public void ForceEyeClosed()
	{
		sharedMaterials[eyeMaterialIndex] = closedMaterial;
		myRenderer.sharedMaterials = sharedMaterials;
		blinking = true;
		nextBlinkTime = Mathf.Infinity;
		nextOpenTime = nextBlinkTime;

	}
}
