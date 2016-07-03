﻿using UnityEngine;
using UnityEngine.UI;

public enum ItemKind
{
	Food,
	Water,
	Grain,
	Ammo
}

public class ItemMain : MonoBehaviour
{
	public Text ValueText;

	public ItemKind ItemKind;
	public InventoryKey Inventory;

	public int Value = 10;

	public void ResetPosition()
	{
		transform.localPosition = Inventory.Position();
	}

	public void Update()
	{
		if (ValueText) ValueText.text = Value.ToString();
	}

	public void OnMouseDown()
	{
		Debug.Log(" mouse down Item:" + ItemKind);
	}

	public void OnMouseUp()
	{
		Debug.Log(" mouse up Item:" + ItemKind);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);

		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.GetComponent<Inventory>())
			{
				//Debug.Log("Hit inventory");
				if (hit.collider.gameObject.GetComponent<Inventory>().AddItem(this))
				{
					return;
				}
			}
		}

		ResetPosition();
	}


	public void OnMouseDrag()
	{

		float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		float z = transform.position.z;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);
		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.GetComponent<Inventory>())
			{
				//Debug.Log("Hit room device");
				z = hit.point.z - 0.25f;
				distance_to_screen = Camera.main.WorldToScreenPoint(hit.point).z;
				break;
			}
		}
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

		transform.position = new Vector3(pos_move.x - 0.25f, pos_move.y - 0.0f, z);

	}

	public static ItemMain Create(ItemKind kind) 
	{
		GameObject obj = Instantiate(GameLogic.Instance.Items[(int)kind]);
		return obj.GetComponent<ItemMain>();
	}
}
