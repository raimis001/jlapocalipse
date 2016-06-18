using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomHydrophonic : RoomDevice
{

	public Vaga[] Vagas;
	public WaterRandomize WaterBoiler;

	public float WaterMax = 50;
	public float WaterStore = 0;

	public float Progress
	{
		get { return WaterStore/WaterMax; }
	}

	protected override void Start()
	{
		base.Start();
		if (Inventory)
		{
			Inventory.gameObject.SetActive(false);
		}
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

	public void OnMouseUp()
	{
		if (Inventory)
		{
			Inventory.gameObject.SetActive(!Inventory.gameObject.activeSelf);
		}
	}

	void CheckInventory()
	{
		if (!Inventory || Inventory.Count < 1) return;
		if (WaterStore >= WaterMax) return;

		foreach (ItemMain item in Inventory.Items.Values)
		{
			if (item.ItemKind == ItemKind.Water)
			{
				if (WaterStore + item.Value > WaterMax) continue;

				WaterStore += item.Value;
				Inventory.RemoveItem(item, true);
				break;
			}
		}
	}

	void ChekVagas()
	{
		foreach (Vaga vaga in Vagas)
		{
			
		}
	}
}
