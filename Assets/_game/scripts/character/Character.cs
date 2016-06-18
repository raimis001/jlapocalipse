using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public Inventory Inventory;

	// Use this for initialization
	void Start()
	{
		Inventory.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonUp(1))
		{
			ItemMain item = GameLogic.CreateItem(Helper.GetRandomEnum<ItemKind>());
			if (!Inventory.AddItem(item))
			{
				Destroy(item.gameObject);
			}
		}
	}

	public void OnMouseUp()
	{
		Debug.Log("Caharcter click");
		Inventory.gameObject.SetActive(!Inventory.gameObject.activeSelf);
	}


}
