using UnityEngine;
using System.Collections;

public enum ItemKind
{
	Food,
	Water
}

public class ItemMain : MonoBehaviour
{

	public ItemKind ItemKind;
	public InventoryKey Inventory;


	public void ResetPosition()
	{
		transform.localPosition = Inventory.Position();
	}

	public void OnMouseDown()
	{

	}

	public void OnMouseUp()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);

		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.GetComponent<Inventory>())
			{
				Debug.Log("Hit inventory");
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
			}
		}
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

		transform.position = new Vector3(pos_move.x - 0.25f, pos_move.y - 0.0f, z);

	}
}
