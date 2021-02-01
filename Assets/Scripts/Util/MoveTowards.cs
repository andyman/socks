using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
	public Transform thingToMove;
	public Transform destination;
	public float speed = 4.0f;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		thingToMove.position = Vector3.MoveTowards(thingToMove.position, destination.position, speed * Time.deltaTime);
	}
}
