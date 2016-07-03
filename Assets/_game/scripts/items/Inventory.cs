using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct IntVector
{
	public int x;
	public int y;
}
[Serializable]
public struct InventoryKey
{
	public int Id;
	public Inventory Parent;

	public Vector3 Position()
	{
		return Parent.CellPosition(Id);
	}
}

public class Inventory : MonoBehaviour
{
	public int SizeX = 2;
	public int SizeY = 2;

	public GameObject[] Cells;

	public List<ItemKind> Accept = new List<ItemKind>();

	public delegate void ChangeInventory();

	public bool AcceptAll = true;
	public ChangeInventory OnChange;

	public bool Visible 
	{
		get { return gameObject.activeSelf; }
		set {
			gameObject.SetActive(value);
		}
	}

	public int Count
	{
		get { return Items.Count; }
	}

	public Dictionary<int, ItemMain> Items = new Dictionary<int, ItemMain>();

	public void RemoveItem(ItemMain item, bool destroy = false)
	{
		Items.Remove(item.Inventory.Id);

		if (destroy) Destroy(item.gameObject);

		if (OnChange != null) OnChange();
	}

	public bool AddItem(ItemMain item)
	{
		if (!AcceptAll && !Accept.Contains(item.ItemKind)) return false;

		int key = FindFree();
		if (key < 0) return false;


		if (item.Inventory.Parent)
		{
			item.Inventory.Parent.RemoveItem(item);
		}

		item.Inventory.Id = key;
		item.Inventory.Parent = this;

		item.transform.SetParent(transform);
		item.transform.localPosition = CellPosition(key);

		Items.Add(key, item);
		if (OnChange != null) OnChange();

		return true;
	}

	public void OnEnable()
	{
		for (int i = 0; i < Cells.Length; i++)
		{
			int y = Mathf.FloorToInt(i/3f);
			int x = i - y*3;
			Cells[i].SetActive(x < SizeX && y < SizeY);
		}
		BoxCollider collider = GetComponent<BoxCollider>();
		collider.size = new Vector3(SizeX * 0.5f, SizeY * 0.5f, 0.1f);
		collider.center = new Vector3(collider.size.x * 0.5f, -collider.size.y * 0.5f + 0.25f, 0.27f);
	}

	int CellId(int x, int y)
	{
		return 0;
	}

	public Vector3 CellPosition(int id)
	{
		int y = Mathf.FloorToInt(id / 3f);
		int x = id - y * 3;
		return new Vector3(x * 0.5f, y * -0.5f);
	}

	int FindFree()
	{
		int result = -1;

		for (int i = 0; i < Cells.Length; i++)
		{
			if (!Cells[i].activeSelf) continue;

			if (!Items.ContainsKey(i) || Items[i] == null)
			{
				result = i;
				break;
			}
		}

		return result;
	}

}
