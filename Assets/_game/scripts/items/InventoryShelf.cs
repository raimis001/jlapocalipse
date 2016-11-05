using UnityEngine;
using System.Collections;
using System;

public class InventoryShelf : MonoBehaviour
{
	public int Width = 3;
	public int Height = 3;

	public GameObject Shelf;

	public Transform Follow;

	internal Inventory Inventory = new Inventory();

	void Start()
	{
		if (!Shelf) return;

		int x = 0;
		int y = 0;
		foreach (Transform plaukts in Shelf.transform)
		{
			//Debug.Log(plaukts.name + "x:" + x + " y:" + y);
			plaukts.gameObject.SetActive(x < Width && y < Height);
			x++;
			if (x > Width)
			{
				x = 0;
				y++;
			}
		}
	}

	void Update()
	{
		if (Follow)
		{
			transform.position = new Vector3(Follow.position.x, Follow.position.y, transform.position.z);
		}
	}

	internal bool AddItem(ItemMain itemMain)
	{
		Inventory.AddItem(itemMain);
		itemMain.transform.SetParent(transform);
		return true;
	}
}
