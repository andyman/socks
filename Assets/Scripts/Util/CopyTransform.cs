using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
	public Transform target;

	// Start is called before the first frame update
	void Start()
	{
		Refresh();

	}

	// Update is called once per frame
	void Update()
	{
		Refresh();
	}

	void FixedUpdate()
	{
		Refresh();

	}
	void LateUpdate()
	{
		Refresh();
	}
	private void Refresh()
	{
		transform.position = target.position;
		transform.rotation = target.rotation;
	}
}
