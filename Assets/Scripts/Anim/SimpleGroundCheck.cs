using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGroundCheck : MonoBehaviour
{
	public bool grounded = false;
	public Animator anim;
	public LayerMask groundLayerMask;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		grounded = Physics.CheckSphere(transform.position, 0.03f, groundLayerMask);
		anim.SetBool("grounded", grounded);
	}
}