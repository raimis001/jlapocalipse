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
			_val = value * 0.8f + 0.2f;
			isBreak = true;
		}
	}

	public Transform Water;
	public Renderer WaterMaterial;

	private bool isTweening;
	private bool isBreak;
	private int direction = 1;

	void Update()
	{

		if (!Application.isPlaying)
		{
			if (Math.Abs(Value - val) > 0.001f) Value = val;
			if (Water)
			{
				Water.localScale = new Vector3(Water.localScale.x, Value, Water.localScale.z);
			}
		}
		else
		{
			if (WaterMaterial)
			{

				float offsetX = Mathf.PingPong(Time.time * 0.15f, 2);
				float offsetY = Mathf.PingPong(Time.time * 0.1f, 2);
				WaterMaterial.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
			}

			StartCoroutine(Randomize());

			Vector3 rot = transform.localEulerAngles;
			rot.x = Mathf.PingPong(Time.time*0.08f, 1f) - 0.5f;
			transform.localEulerAngles = rot;
		}

	}

	IEnumerator Randomize()
	{
		if (!Water || isTweening) yield break;

		isTweening = true;
		isBreak = false;

		float delta = Mathf.Clamp(_val + Random.Range(0, 0.01f)*direction,0f,1f);
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
