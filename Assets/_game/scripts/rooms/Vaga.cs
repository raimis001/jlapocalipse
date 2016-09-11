using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum GrowStatus
{
	None,
	Growing,
	Ripe,
	Riping
}

[ExecuteInEditMode]
public class Vaga : MonoBehaviour
{

	public float GrowMax = 30;

	[Range(0, 1f)]
	public float GrowStage;
	private float _growStage;

	internal GrowStatus GrowStatus;
	internal float GrowTime;
	
	public Transform Laksti;
	public GameObject Light;

	public raWaypoint SpawnPoint;
	public Animator Harvester;

	public delegate void VagaEvent(Vaga vaga);
	public VagaEvent OnEndGrow;

	void Start()
	{
		GrowStage = 0;
		GrowTime = 0;
		GrowStatus = GrowStatus.None;
		Light.SetActive(false);
		SetLaksti();
	}

	void Update()
	{
		if (!Application.isPlaying)
		{
			//Editor mode
			if (Math.Abs(_growStage - GrowStage) > 0.01f)
			{
				_growStage = GrowStage;
				SetLaksti();
			}
			return;
		}

		if (GrowStatus == GrowStatus.Growing)
		{
			GrowTime += Time.deltaTime;
			GrowStage = GrowTime/GrowMax;
			if (GrowStage >= 1)
			{
				GrowTime = 0;
				GrowStage = 1;
				GrowStatus = GrowStatus.Ripe;
				Light.SetActive(false);

				if (Harvester)
				{
					AnimationEvent.OnAnimEvent += AnimEvent;
					Harvester.SetTrigger("Harvest");
				}

			}
			SetLaksti();
		}
	}

	void AnimEvent(string tag)
	{
		AnimationEvent.OnAnimEvent -= AnimEvent;
		Debug.Log("End anim:" + tag);

		if (tag.Equals("EndHarvest"))
		{
			if (OnEndGrow != null) OnEndGrow(this);
			GrowStatus = GrowStatus.None;
		}

	}

	void SetLaksti()
	{
		if (Laksti)
		{
			foreach (Transform laksti in Laksti.transform)
			{
				Plant plant = laksti.GetComponent<Plant>();
				if (plant) plant.Grow = GrowStage;
			}
		}
	}

	void PlantSeed(RawFood food)
	{
		if (!Laksti) return;
		foreach (Transform laksti in Laksti.transform)
		{
			Destroy(laksti.gameObject);
		}

		GameObject prefab = GameLogic.GetFood(food);
		if (!prefab)
		{
			return;
		}
		//0, 0.75, 1.5
		float x = 0;
		float y = 0;
		for (int i = 0; i < 12; i++)
		{
			Vector3 pos = new Vector3(x + Random.Range(-0.2f, 0.2f),0, y + Random.Range(-0.1f, 0.1f));
			GameObject obj = Instantiate(prefab);
			if (obj)
			{
				obj.transform.SetParent(Laksti);
				obj.transform.localPosition = pos;
			}
			if (x > 0.75f)
			{
				y += 0.7f;
				x = 0;
			}
			else
			{
				x += 0.75f;
			}
		}
	}

	public void OnMouseUp()
	{
		Debug.Log("Vaga click:" + GrowStatus);

		if (GrowStatus == GrowStatus.None)
		{
			GrowTime = 0;
			GrowStatus = GrowStatus.Growing;
			PlantSeed(RawFood.Potato);

			Light.SetActive(true);
			SetLaksti();
		}

	}
}
