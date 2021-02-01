using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantTranslation : MonoBehaviour
{
	public Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
		transform.Translate(velocity * Time.deltaTime);        
    }
}
