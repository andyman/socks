using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titleer : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Jump"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
	}
}
