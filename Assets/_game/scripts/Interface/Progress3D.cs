using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Progress3D : MonoBehaviour
{
	public Transform Bar;

	[Range(0, 1f)]
	public float Value;

	void Start()
	{
		Bar.localScale = new Vector3(Bar.localScale.x, Value, Bar.localScale.z);
		Bar.localPosition = new Vector3(Bar.localPosition.x, Value - 1f, Bar.localPosition.z);
	}

	void Update()
	{
		if (Bar)
		{
			Bar.localScale = new Vector3(Bar.localScale.x, Value, Bar.localScale.z);
			Bar.localPosition = new Vector3(Bar.localPosition.x, Value - 1f, Bar.localPosition.z);
		}
	}
}
