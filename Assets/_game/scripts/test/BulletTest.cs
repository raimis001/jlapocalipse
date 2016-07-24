using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class BulletTest : MonoBehaviour
{

	public GameObject BulletPrefab;
	public Transform BulletsHolder;

	[Range(0,1)]
	public float ShotingRate;

	[Range(0.5f, 1)] 
	public float Snipper;

	private float _snipper;


	private bool isShooting;
	private float shotingTime;

	private List<WeightClass<float>> Target = new List<WeightClass<float>>();



	void Start()
	{
		float c = 0.01f;
		/*
			float e = 0.2f;
			for (float i = c; i < e; i += c)
			{
				float val = 20f*(i - c) + 5f * (1f - e);
				Target.Add(Weighted.Create(i, val));
			}

			Target.Sort((a, b) => a.Weight.CompareTo(b.Weight));
			*/


		for (float i = 0; i <= 1f; i += c)
		{
			Target.Add(Weighted.Create(0,i));
		}
		RecalcSnipper();
	}

	void RecalcSnipper()
	{
		Weighted.TotalWeight = 0;
		foreach (WeightClass<float> weightClass in Target)
		{
			weightClass.Weight = weightClass.Value > Snipper ? 0.01f : Snipper - weightClass.Value;
			Weighted.TotalWeight += weightClass.Weight;
		}

		Target.Sort((a, b) => a.Weight.CompareTo(b.Weight));

	}

	public void Update()
	{
		//if (!isShooting) return;

		if (Snipper != _snipper)
		{
			_snipper = Snipper;
			RecalcSnipper();
		}

		shotingTime -= Time.deltaTime;
		if (shotingTime <= 0)
		{
			shotingTime = ShotingRate;
			DoShot();
		}

	}

	public void OnMouseUp()
	{
		Debug.Log("Mouse upped");
		isShooting = false;
	}

	public void OnMouseDown()
	{
		isShooting = true;
	}

	void DoShot()
	{
		GameObject bullet = Instantiate(BulletPrefab);

		
		bullet.transform.SetParent(BulletsHolder);
		bullet.transform.localPosition = new Vector3(0, Target.GetWeighted()*10f*Mathf.Sign(UnityEngine.Random.Range(-1, 1)) + 10f, Target.GetWeighted()*10f* Mathf.Sign(UnityEngine.Random.Range(-1, 1)) + 10f);
		//bullet.transform.localPosition = new Vector3(0, 10f * Snipper + 20f * Random.Range(0,1f - Snipper), 10f * Snipper + 20f * Random.Range(0, 1f - Snipper));
		bullet.transform.localScale = Vector3.one;

		Destroy(bullet, 1f);
	}
}
