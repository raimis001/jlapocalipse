using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class ProgressMk : ProgressBar
{

	[Range(0,1)]
	public float testValue;

	private Material MkMaterial;


	void Start() 
	{
	}

	void Update() 
	{ 
		if (!Application.isPlaying) 
		{
			Value = testValue;	
		}
	}

	protected override void OnValueChange()
	{
		Color color = Value > 0 ? Color.Lerp(Color.red, Color.green, Value) : Color.black;
		if (!MkMaterial)
		{
			MkMaterial = GetComponent<Renderer>().material;
		}
		MkMaterial.SetColor("_MKGlowTexColor", color);
	}

}
