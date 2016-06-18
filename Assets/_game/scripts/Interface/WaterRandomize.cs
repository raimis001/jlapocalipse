using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class WaterRandomize : MonoBehaviour
{

	[Range(0,1f)]
	public float val;

	private float _val;
	public float Value
	{
		get { return _val; }
		set
		{
			_val = value;
			isBreak = true;
		}
	}

	public Transform Water;

	private bool isTweening;
	private bool isBreak;
	private int direction = 1;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Math.Abs(Value - val) > 0.001f) Value = val;

		if (!Application.isPlaying && Water)
		{
			Water.localScale = new Vector3(Water.localScale.x, Value, Water.localScale.z);
			return;
		}

		if (!Water || isTweening) return;




		StartCoroutine(Randomize());
	}

	IEnumerator Randomize()
	{
		isTweening = true;
		isBreak = false;

		float delta = Mathf.Clamp(val + Random.Range(0, 0.01f)*direction,0f,1f);
		Vector3 scale = Water.localScale;

		while (Mathf.Abs(Water.localScale.y - delta) > 0.01f)
		{
			scale.y = Mathf.Lerp(scale.y, delta, 0.01f);
			Water.localScale = scale;
			if (isBreak) break;

			yield return null;
		}

		direction *= -1;
		isBreak = false;
		isTweening = false;
	}
}
