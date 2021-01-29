using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCalculator : MonoBehaviour {

	public string speedParameter;
	public string forwardSpeedParameter;
	public string rightSpeedParameter;
	public string upSpeedParameter;

	public float speed = 0.0f;
	public float dampTime = 0.1f;

	public bool useFlatSpeed = false;

	public Animator animator;
	private int speedHash = -1;
	private int forwardSpeedHash = -1;
	private int rightSpeedHash = -1;
	private int upSpeedHash = -1;

	private Vector3 lastPosition;
	public bool useLocalPosition = false;

	public Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
		speedHash = speedParameter == "" ? -1 : Animator.StringToHash(speedParameter);
		forwardSpeedHash = forwardSpeedParameter == "" ? -1 : Animator.StringToHash(forwardSpeedParameter);
		rightSpeedHash = rightSpeedParameter == "" ? -1 : Animator.StringToHash(rightSpeedParameter);
		upSpeedHash = upSpeedParameter == "" ? -1 : Animator.StringToHash(upSpeedParameter);
		lastPosition = GetPosition();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 newPosition = GetPosition();
		Vector3 diff = newPosition - lastPosition;
		if (Time.deltaTime > 0.0f)
		{
			velocity = (diff / Time.deltaTime);
			speed = diff.magnitude / Time.deltaTime;
		}


		if (useFlatSpeed)
		{
			diff.y = 0.0f;
		}
		Vector3 dir = diff.normalized;

		lastPosition = newPosition;
		if (speedHash != -1)
		{
			if (animator != null)
				animator.SetFloat(speedHash, speed, dampTime, Time.deltaTime);
		}

		if (forwardSpeedHash != -1)
		{
			float forwardSpeed = Vector3.Dot(transform.forward, dir) * speed;
			if (animator != null)
				animator.SetFloat(forwardSpeedHash, forwardSpeed, dampTime, Time.deltaTime);
		}

		if (rightSpeedHash != -1)
		{
			float rightSpeed = Vector3.Dot(transform.right, dir) * speed;
			if (animator != null)
				animator.SetFloat(rightSpeedHash, rightSpeed, dampTime, Time.deltaTime);
		}

		if (upSpeedHash != -1)
		{
			float upSpeed = Vector3.Dot(transform.up, dir) * speed;
			if (animator != null)
				animator.SetFloat(upSpeedHash, upSpeed, dampTime, Time.deltaTime);
		}
	}
	private Vector3 GetPosition()
	{
		return (useLocalPosition ? transform.localPosition : transform.position);
	}
}
