using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachAllChildren : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		transform.DetachChildren();
		enabled = false;
	}



}
