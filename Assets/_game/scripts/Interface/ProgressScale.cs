using UnityEngine;
using System.Collections;

public enum ProgressKind 
{
	Vertical,
	Horizontal
}

[ExecuteInEditMode]
public class ProgressScale : ProgressBar
{
	public Transform Bar;
	public ProgressKind Kind;

	[Range(0, 1f)]
	public float Test;

	protected override void OnValueChange()
	{
		if (Bar)
		{
			Bar.localScale = new Vector3(Kind == ProgressKind.Horizontal ? Value : Bar.localScale.x, Kind == ProgressKind.Vertical ? Value : Bar.localScale.y, Bar.localScale.z);

			//Bar.localPosition = new Vector3(Bar.localPosition.x, Value - 1f, Bar.localPosition.z);
		}
	}

	void Start()
	{
		Value = Test;
	}

	void Update()
	{
		if (!Application.isPlaying)
		{
			Value = Test;
		}
	}

}
