using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingoFlapper : MonoBehaviour
{
	public Transform body;
	public Transform leftWing;
	public Transform rightWing;

	public float flapRate;
	private float flapTime = 0.0f;
	public Vector2 flapVariability;
	public float flapDownDegrees = 30.0f;
	public float flapUpDegrees = 45.0f;

	private Rigidbody rb;
	private float lastVy;
	public float boostFlapMultiplier = 2.0f;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		float vy = Mathf.Lerp(lastVy, rb.velocity.y, 1f * Time.deltaTime);

		float normalizedUpDown = Mathf.InverseLerp(-5.0f, 5.0f, vy);
		float upDownSpeedMultiplier = Mathf.Lerp(0.0f, 2.0f, normalizedUpDown);

		float boostFlap = Input.GetButton("Jump") ? boostFlapMultiplier : 1.0f;

		flapTime += Time.deltaTime * Random.Range(flapVariability.x, flapVariability.y) * upDownSpeedMultiplier * boostFlap;


		float lerp = Mathf.Sin(flapTime * Mathf.PI * 2.0f * flapRate) * 0.5f + 0.5f;
		leftWing.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(flapDownDegrees, -flapUpDegrees, lerp));
		rightWing.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(-flapDownDegrees, flapUpDegrees, lerp));

		lastVy = vy;



	}
}
