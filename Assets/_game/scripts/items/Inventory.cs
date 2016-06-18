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

	public Dictionary<int, ItemMain> Items = new Dictionary<int, ItemMain>();

	public void RemoveItem(ItemMain item)
	{
		Items.Remove(item.Inventory.Id);
	}

	public bool AddItem(ItemMain item)
	{
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

	}

	int CellId(int x, int y)
	{
		return 0;
	}

	public Vector3 CellPosition(int id)
	{
		int y = Mathf.FloorToInt((float)id / SizeX);
		int x = id - y * SizeX;

		return new Vector3(x * 0.5f, y * -0.5f);
	}

	int FindFree()
	{
		int result = -1;
		int size = SizeX * SizeY;
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
