using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkDisperser : MonoBehaviour
{
	public BoxCollider box;
	public int junkCount = 200;

	public LayerMask raycastMask;
	public Transform[] junkPrefabs;
	public Gradient gradient;

	// Start is called before the first frame update
	void Start()
	{
		//Generate();
	}

	[ContextMenu("Generate")]
	void Generate()
	{
		Bounds b = box.bounds;
		MaterialPropertyBlock mpb = new MaterialPropertyBlock();

		for (int i = 0; i < junkCount; i++)
		{
			Vector3 pos = new Vector3(
				Random.Range(b.min.x, b.max.x),
				b.max.y,
				Random.Range(b.min.z, b.max.z)
				);

			if (Physics.Raycast(pos, Vector3.down, out RaycastHit hitInfo, 20.0f, raycastMask))
			{
				pos = hitInfo.point;
				Quaternion rot = Quaternion.LookRotation(Random.onUnitSphere);

				Transform prefab = junkPrefabs[Random.Range(0, junkPrefabs.Length)];
				Transform t = Instantiate<Transform>(prefab, pos, rot, transform);
				t.localScale = Vector3.one * Random.Range(1.0f, 2.0f);
				mpb.SetColor("_Color", Random.ColorHSV(0.0f, 1.0f, 0.0f, 0.5f, 0.0f, 1.0f));
				t.GetComponent<MeshRenderer>().SetPropertyBlock(mpb);

			}

		}
	}

}
