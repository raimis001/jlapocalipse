using UnityEngine;
using System.Collections;

public class RoomFood : RoomDevice
{

	public Transform DropPoint;
	public Animator Animator;

	public ProgressBar RawBar;
	public ProgressBar MealBar;

	public float RawMax = 50;
	public float RawCount = 50;
	public float RawTime = 5f;
	public float RawNeed = 2;
	private float _rawTime;

	public float MealMax = 20;
	public float MealCount = 0;
	public float MealTime = 7;
	public float MealNeed = 4;
	private float _mealTime;

	//public InventoryClass FoodInventory;

	protected override void Start()
	{
		base.Start();
		_rawTime = RawTime;
		_mealTime = MealTime;
	}

	protected override void Update()
	{
		base.Update();

		if (RawBar) RawBar.Value = RawCount / RawMax;
		if (MealBar) MealBar.Value = MealCount / MealMax;

		if (!Working) return;

		_rawTime -= Time.deltaTime;
		if (_rawTime <= 0) {
			MakeRaw();
		}

		if (MealCount < MealNeed) return;

		_mealTime -= Time.deltaTime;
		if (_mealTime <= 0) {
			MakeMeal();
		}

	}

	void MakeRaw() {
		if (RawCount < RawNeed) return;
		if ((MealCount + 1) > MealMax) return;

		RawCount -= RawNeed;
		MealCount++;

		_rawTime = RawTime;
	}

	void MakeMeal() {
		if (MealCount < MealNeed) return;
		//if (FoodInventory.Count >= 2) return;

		MealCount -= MealNeed;
		DropItem();

	}

	public void OnMouseUp()
	{
		Debug.Log("Drop item");
		DropItem();
	}

	void DropItem()
	{
		ItemMain item = ItemMain.Create(ItemKind.Food);
		//FoodInventory.AddItem(item);

		item.transform.position = DropPoint.position;
		Animator.SetTrigger("Work");
	}
}
