using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;
	public float speed = 4.0f;
	public float turnLerpRate = 10.0f;
	public float stealthSpeed = 1.0f;

	private Vector2 inputs;
	private Transform camTran;
	private bool sneaking = false;
	public Animator anim;
	public bool grounded;

	public float jumpSpeed;
	private bool jumpPressed;
	private float nextJumpTime;

	// Start is called before the first frame update
	void Start()
	{
		if (rb == null)
		{
			rb = GetComponentInChildren<Rigidbody>();
		}
		if (camTran == null || !camTran.gameObject.activeSelf)
		{
			camTran = Camera.main.transform;
		}
	}

	// Update is called once per frame
	void Update()
	{
		inputs.x = Input.GetAxis("Horizontal");
		inputs.y = Input.GetAxis("Vertical");

		if (inputs.magnitude > 1.0f)
		{
			inputs.Normalize();
		}

		jumpPressed = Input.GetButtonDown("Jump");
		sneaking = Input.GetButton("Sneak");
		anim.SetBool("sneaking", sneaking);
		grounded = anim.GetBool("grounded");
	}

	private void FixedUpdate()
	{
		Vector3 v = rb.velocity;

		// translation
		Vector3 camForward = camTran.forward;
		camForward.y = 0.0f;
		camForward.Normalize();
		Quaternion camForwardRot = Quaternion.LookRotation(camForward);

		float tempSpeed = sneaking ? stealthSpeed : speed;

		v = camForwardRot * new Vector3(inputs.x, 0.0f, inputs.y) * tempSpeed;
		v.y = rb.velocity.y;

		if (grounded && jumpPressed && Time.time > nextJumpTime)
		{
			v.y = jumpSpeed;
			jumpPressed = false;
			grounded = false;
			nextJumpTime = Time.time + 0.5f;
		}
		rb.velocity = v;


		// rotate to face movement
		if (v.magnitude > 0.1f)
		{
			Vector3 flatV = v;
			v.y = 0.0f;
			flatV.Normalize();
			if (flatV.magnitude > 0.1f)
			{
				Quaternion rot = rb.rotation;
				Quaternion targetRot = Quaternion.LookRotation(flatV);
				rb.rotation = Quaternion.Slerp(rot, targetRot, Time.deltaTime * turnLerpRate);
			}
		}
	}
}
