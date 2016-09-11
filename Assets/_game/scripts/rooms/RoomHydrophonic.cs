using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomHydrophonic : RoomDevice
{

	public raWaypointPath FoodPath;

	public Vaga[] Vagas;
	public WaterRandomize WaterBoiler;

	public float WaterMax = 50;
	public float WaterStore = 0;

	[Header("Animation")]
	public TextureRotate TransporterU;
	public TextureRotate TransporterD;

	public float Progress
	{
		get { return WaterStore/WaterMax; }
	}

	protected override void Start()
	{
		base.Start();
		foreach (Vaga vaga in Vagas)
		{
			vaga.OnEndGrow = OnEndGrow;
		}
		/*
		if (Inventory)
		{
			Inventory.gameObject.SetActive(false);
		}
		*/
	}

	protected override void Update()
	{
		base.Update();

		CheckInventory();

		if (WaterBoiler)
		{
			WaterBoiler.Value = Progress;
		}
	}

	void OnEndGrow(Vaga vaga)
	{
		CreateFood(ItemKind.Food,vaga.SpawnPoint.Index);
	}

	public void OnMouseUp()
	{
		Debug.Log("Click on hydrophonic");
		//CreateFood(ItemKind.Food);
	}

	void CreateFood(ItemKind kind, int waypoint)
	{
		ItemMain item = ItemMain.Create(kind);
		StartCoroutine(FoodMove(item,waypoint, 3));
	}


	void CheckInventory()
	{
		/*
		if (!Inventory || Inventory.Items.Count < 1) return;
		if (WaterStore >= WaterMax) return;

		foreach (ItemMain item in Inventory.Items.GetItems(ItemKind.Water))
		{
			if (WaterStore + item.Value > WaterMax) continue;

			WaterStore += item.Value;
			Inventory.RemoveItem(item, true);
			break;
		}
		*/
	}

	void ChekVagas()
	{
	}

	IEnumerator FoodMove(ItemMain food, int startPosition, int endPosition)
	{
		if (!FoodPath.isWaypoint(startPosition)) yield break;
		if (!FoodPath.isWaypoint(endPosition)) yield break;

		food.Enabled = false;

		food.transform.position = FoodPath.position(startPosition);
		Vector3 destination = FoodPath.position(endPosition);

		float speed = 1f;

		TransporterU.Working = true;
		TransporterD.Working = true;

		while (Vector3.Distance(food.transform.position, destination) > 0.01f)
		{
			food.transform.position = Vector3.MoveTowards(food.transform.position, destination, Time.deltaTime*speed);
			yield return null;
		}

		food.transform.position = destination;

		food.Enabled = true;
		TransporterU.Working = false;
		TransporterD.Working = false;
	}
}
