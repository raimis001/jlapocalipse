using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour
{

	internal Inventory Inventory;
	internal float Energy = 0;
	internal bool DoCharge;
	//private float ChargeTime = 1f;
	private float ChargeTemp;
	private const float ChargeValue = 0.3f;


	public WeaponControler Weapon;

	public ProgressBar EnergyProgress;

	void Start()
	{
		Inventory = new Inventory() { MaxCount = 9 };
		Inventory.OnChange += OnInventoryChange;
	}

	void Update()
	{
		if (DoCharge && Energy < 1)
		{
			Energy += Time.smoothDeltaTime*ChargeValue;
			if (Energy > 1) Energy = 1;
			DoCharge = false;
		}

		if (EnergyProgress)
		{
			EnergyProgress.Value = Energy;
		}
	}

	void OnInventoryChange()
	{
		//Rigidbody.mass = 10 + Inventory.Items.Count * 3;
		CheckInventory();
	}

	void CheckInventory()
	{
		if (!Weapon) return;
		if (Inventory.Count < 1) return;
		if (Weapon.Ammo + 20 > Weapon.AmmoCount) return;

		foreach (InventoryItem item in Inventory.GetItems(ItemKind.Ammo))
		{
			if (Weapon.Ammo + item.count > Weapon.AmmoCount) continue;

			Weapon.Ammo += item.count;
			Inventory.RemoveItem(item);
			break;
		}
	}

}
