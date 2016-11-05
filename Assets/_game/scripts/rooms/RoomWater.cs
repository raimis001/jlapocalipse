using UnityEngine;
using System.Collections;

public class RoomWater : RoomDevice
{

	public Animator WorkAnimator;
	public Transform DropSpot;
	public ProgressBar FillTank;
	public ProgressBar WaterTank;

	const float _workingTime = 10;

	bool _makeWater;

	void Awake()
	{
		FillTank.Value = 0;
		WaterTank.Value = 0;
	}
	protected override void Start()
	{
		base.Start();
	}
	override protected void Update()
	{
		base.Update();

		if (!Working) return;
		if (Inventory == null) return;

		if (Inventory.Count < 5 && FillTank.Value >= 1 && !_makeWater)
		{
			//TODO: Working
			FillTank.Value = 0;
			StartCoroutine(MakeWater());
			
			return;
		}
		FillTank.Value += Time.deltaTime / _workingTime;
	}

	IEnumerator MakeWater()
	{
		if (_makeWater) yield break;
		_makeWater = true;

		WaterTank.Value = 0;
		while (WaterTank.Value < 1)
		{
			WaterTank.Value += Time.smoothDeltaTime / 1f;
			yield return null;
		}

		WaterTank.Value = 0;
		ItemMain item = ItemMain.Create(ItemKind.Water, DropSpot.position);
		Inventory.AddItem(item);

		yield return new WaitForSeconds(0.5f);
		if (WorkAnimator)
		{
			WorkAnimator.SetTrigger("Work");
		}
		_makeWater = false;
	}

	public void OnMouseDown()
	{
		Debug.Log("Water processor click");
	}
}
