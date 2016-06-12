using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Progress3D : MonoBehaviour
{
	public Transform Bar;

	[Range(0, 1f)]
	public float Test;

	public float Value
	{
		set
		{
			float val = Mathf.Clamp(value, 0f, 1f);
			if (Bar)
			{
				Bar.localScale = new Vector3(Bar.localScale.x, val, Bar.localScale.z);
				Bar.localPosition = new Vector3(Bar.localPosition.x, val - 1f, Bar.localPosition.z);
			}
		}
	}

	void Start()
	{
		Value = 0;
	}

	void Update()
	{
		if (!Application.isPlaying)
		{
			Value = Test;
		}
	}

}
