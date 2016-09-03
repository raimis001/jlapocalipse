using UnityEngine;
using System.Collections;

public class InventoryShelf : MonoBehaviour
{
	/*
	public GameObject[] Cells;
	public InventoryClass Items = new InventoryClass();

	public ChangeInventory OnChange;

	public bool Visible {
		get { return gameObject.activeSelf; }
		set {
			gameObject.SetActive(value);
		}
	}

	public void OnEnable()
	{
		for (int i = 0; i < Cells.Length; i++)
		{
			int y = Mathf.FloorToInt(i / 3f);
			int x = i - y * 3;
			Cells[i].SetActive(x < Items.SizeX && y < Items.SizeY);
		}
		BoxCollider collider = GetComponent<BoxCollider>();
		collider.size = new Vector3(Items.SizeX * 0.5f, Items.SizeY * 0.5f, 0.1f);
		collider.center = new Vector3(collider.size.x * 0.5f, -collider.size.y * 0.5f + 0.25f, 0.27f);

		Items.OnChange = OnChange;
	}

	public void RemoveItem(ItemMain item, bool destroy = false)
	{
		Items.RemoveItem(item);
		if (destroy) Destroy(item.gameObject);
	}

	public bool AddItem(ItemMain item)
	{
		string key = Items.AddItem(item);
		if (key.Equals("0")) return false;

		//if (item.Inventory.Parent)
		{
			//item.Inventory.Parent.RemoveItem(item);
		}
		item.Inventory.Id = key;
		//item.Inventory.Parent = this;

		item.transform.SetParent(transform);
		item.transform.localPosition = CellPosition(key);

		return false;
	}

	int CellId(int x, int y)
	{
		return 0;
	}

	public Vector3 CellPosition(string id)
	{
		int y = Mathf.FloorToInt(id / 3f);
		int x = id - y * 3;
	

		string[] s = id.Split(':');
		if (s.Length < 2) return Vector3.zero;

		int x = int.Parse(s[0]);
		int y = int.Parse(s[1]);

		return new Vector3(x * 0.5f, y * -0.5f);
	}

*/
}
