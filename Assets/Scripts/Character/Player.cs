using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Rigidbody rb;

	private bool jumpPressed = false;
	private bool jumpHeld = false;
	private Vector3 inputs = Vector3.zero;

	public LayerMask groundLayerMask;
	public bool grounded = false;
	public float speed = 8.0f;
	public float jumpSpeed = 10.0f;

	private float lastGroundedTime = 0.0f;

	private Camera mainCam;
	public Animator anim;

	public float rotationLerpRate = 10.0f;
	public float jumpHeldGravity = 0.0f;
	public float fallingGravity = 3.0f;

	public AudioClip jumpSound;
	private float nextJumpSoundTime = 0.0f;


	// Start is called before the first frame update
	void Start()
	{
		mainCam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		jumpPressed = Input.GetButtonDown("Jump");
		jumpHeld = Input.GetButton("Jump");

		inputs.x = Input.GetAxis("Horizontal");
		inputs.z = Input.GetAxis("Vertical");

		// limit diagonal speed
		if (inputs.magnitude >= 1.0f)
		{
			inputs.Normalize();
		}

		anim.SetBool("grounded", grounded || Time.time < (lastGroundedTime + 0.25f));
	}

	private void FixedUpdate()
	{

		grounded = Physics.CheckSphere(transform.position, 0.03f, groundLayerMask);
		if (grounded)
		{
			lastGroundedTime = Time.time;
			anim.SetBool("grounded", true);
		}

		// movement

		// get the flat forward of camera
		Vector3 camForward = mainCam.transform.forward;
		camForward.y = 0.0f;
		camForward.Normalize();

		Quaternion camDir = Quaternion.LookRotation(camForward);
		// set the velocity
		Vector3 v = Vector3.zero;
		Vector3 camV = camDir * inputs;
		v = camV * speed;
		v.y = rb.velocity.y;


		// jump
		if (jumpHeld && (grounded || Time.time < lastGroundedTime + 0.25f))
		{
			v.y = jumpSpeed;
			grounded = false;
			lastGroundedTime = 0.0f;
			PlayJumpSound();
		}

		if (!grounded)
		{
			// lesser gravity because we want to jump farther
			if (jumpHeld && v.y >= 0.0f)
			{
				v.y += Time.deltaTime * jumpHeldGravity;
			}
			// more gravity
			else
			{
				v.y += Time.deltaTime * fallingGravity;
			}
		}

		// rotate if we have speed
		if (camV.magnitude > 0.1f)
		{
			Vector3 newFacing = camV.normalized;
			Quaternion targetRot = Quaternion.LookRotation(newFacing);

			rb.rotation = Quaternion.Slerp(rb.rotation, targetRot, Time.deltaTime * rotationLerpRate);
		}
		rb.velocity = v;
	}

	public void PlayJumpSound()
	{
		if (jumpSound != null && Time.time > nextJumpSoundTime)
		{
			ProcAudioSource.instance.PlayOneShot(jumpSound, transform.position, 0.01f, Random.Range(0.80f, 1.2f)).transform.parent = transform;
			//ProcAudioSource.Play(jumpSound, transform.position, 0.01f, Random.Range(0.80f, 1.2f), transform);
			nextJumpSoundTime = Time.time + 0.2f;
		}
	}
}
