using System;
using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Plant : MonoBehaviour
{

	[Range(0, 1)]
	public float grow;

	public Transform PlantTransform;
	public GameObject Blossom;

	private float _grow;
	public float Grow {
		get { return _grow; }
		set {
			_grow = value;
			if (PlantTransform)
			{
				PlantTransform.localScale = Vector3.one * _grow;
			}
			if (Blossom) Blossom.SetActive(_grow > 0.98f);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("Plant trigered:" + other.name);
		if (other.name.Equals("Harvest"))
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		if (!Application.isPlaying)
		{
			if (Math.Abs(grow - _grow) > 0.001f)
			{
				Grow = grow;
			}
		}
	}


}
