using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
	// returns a random value between x and y of the Vector2
	public static float Random(this Vector2 value)
	{
		return UnityEngine.Random.Range(value.x, value.y);
	}

	// flattens along the Y and normalizes to give a direction along the xz plane
	public static Vector3 FlattenYAndNormalize(this Vector3 value)
	{
		value.y = 0.0f;
		value.Normalize();
		return value;
	}
}
