using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cached : MonoBehaviour {
	public static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

	public void OnEnable()
	{
		if (cache.ContainsKey(gameObject.name))
		{
			cache[gameObject.name] = gameObject;
		}
		else
		{
			cache.Add(gameObject.name, gameObject);
		}
	}

	public void OnDisable()
	{
		if (cache.ContainsKey(gameObject.name) && cache[gameObject.name] == gameObject)
		{
			cache.Remove(gameObject.name);
		}
	}

	public static GameObject Find(string key)
	{
		if (cache.ContainsKey(key))
		{
			return cache[key];
		}

		return null;
	}

	public static Transform FindTransform(string key)
	{
		GameObject result = Find(key);
		return (result == null) ? null : result.transform;
	}
}
