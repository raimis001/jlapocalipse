using System.Collections.Generic;

public class InventoryItem
{
	public Inventory parent;
	public ItemKind item;
	public int count;
	public RoomPosition position;
}

public class Inventory
{
	private static int StoredId = 0;
	private static Dictionary<int, Inventory> InventoryList = new Dictionary<int, Inventory>();

	public delegate void InventoryChange();
	public event InventoryChange OnChange;

	public int ID;
	public int MaxCount = 9;

	private Dictionary<int, InventoryItem> Items = new Dictionary<int, InventoryItem>();

	public Inventory()
	{
		ID = StoredId++;
		InventoryList.Add(ID, this);
	}

	public int Count
	{
		get { return Items.Count; }
	}

	private int FreeIndex()
	{
		int i = 0;
		for (;;)
		{
			if (!Items.ContainsKey(i) || Items[i] == null)
			{
				return i;
			}
			i++;
			if (i >= MaxCount)
			{
				return -1;
			}
		}


	}

	public InventoryItem CreateItem(ItemKind item)
	{
		int i = FreeIndex();
		if (i < 0)
		{
			return null;
		}

		InventoryItem inve = new InventoryItem() {parent = this, item = item, count = 1};
		if (Items.ContainsKey(i))
		{
			Items[i] = inve;
		}
		else
		{
			Items.Add(i, inve);
		}
		return inve;
	}

	public bool AddItem(ItemMain item)
	{
		if (item.Inventory != null)
		{
			AddItem(item.Inventory);
		}
		else
		{
			item.Inventory = CreateItem(item.ItemKind);
		}
		
		return true;
	}
	
	public bool AddItem(InventoryItem item)
	{
		int i = FreeIndex();
		if (i < 0)
		{
			return false;
		}
		if (item.parent != null)
		{
			item.parent.RemoveItem(item);
		}
		item.parent = this;

		if (Items.ContainsKey(i))
		{
			Items[i] = item;
		}
		else
		{
			Items.Add(i, item);
		}

		return true;
	}


	public bool RemoveItem(InventoryItem item)
	{
		foreach (KeyValuePair<int, InventoryItem> pair in Items)
		{
			if (pair.Value == item)
			{
				Items.Remove(pair.Key);
				return true;
			}
		}
		return false;
	}

	public void Clear()
	{
		
	}

	public IEnumerable<InventoryItem> GetItems(ItemKind item)
	{
		foreach (InventoryItem value in Items.Values)
		{
			if (value.item == item)
			{
				yield return value;
			}
		}
	}

}