using UnityEngine;
using System.Collections;

public class ProgressColor : ProgressBar
{
	public Gradient Colors;

	public Renderer MaterialRender;

	protected override void OnValueChange()
	{
		MaterialRender.material.color = Colors.Evaluate(Value);
	}
}
