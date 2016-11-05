using UnityEngine;
using UnityEngine.UI;

public enum ItemKind
{
	Food,
	Water,
	Grain,
	Ammo
}

public enum RawFood
{
	Potato, 
	Carrot,
	Corn
}

public class ItemMain : MonoBehaviour
{
	public Text ValueText;

	public ItemKind ItemKind;

	public int Value = 10;

	internal bool Enabled = true;

	internal InventoryItem Inventory;

	Vector3 _resetPosition;

	public void ResetPosition()
	{
		transform.localPosition = _resetPosition;
	}

	public void Update()
	{
		if (ValueText) ValueText.text = Value.ToString();
	}

	public void OnMouseDown()
	{
		Debug.Log(" mouse down Item:" + ItemKind);
		_resetPosition = transform.position;
	}

	public void OnMouseUp()
	{
		if (!Enabled) return;
		Debug.Log(" mouse up Item:" + ItemKind);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);

		foreach (RaycastHit hit in hits)
		{
			//Debug.Log(hit.collider.name);
			InventoryShelf shelf = hit.collider.gameObject.GetComponentInParent<InventoryShelf>();
			if (shelf)
			{
				Debug.Log("Hit inventory");
				if (shelf.AddItem(this))
				{
					return;
				}
			}
		}

		ResetPosition();
	}


	public void OnMouseDrag()
	{
		if (!Enabled) return;

		float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		float z = transform.position.z;

		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);
		foreach (RaycastHit hit in hits)
		{
			InventoryShelf shelf = hit.collider.gameObject.GetComponentInParent<InventoryShelf>();
			if (shelf)
			{
				Debug.Log("Hit invenrtory");
				z = shelf.transform.position.z - 0.0f;
				distance_to_screen = Camera.main.WorldToScreenPoint(hit.point).z;
				break;
			}
		}
		
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
		transform.position = new Vector3(pos_move.x - 0.25f, pos_move.y - 0.0f, z);

	}

	public static ItemMain Create(ItemKind kind, Vector3 position = new Vector3())
	{
		if (!GameLogic.Instance) return null;
		int i = (int) kind;
		//Debug.Log("Get kind:" + kind + " int:" + i + " len:" + GameLogic.Instance.Items.Length + " prefab:" + (GameLogic.Instance.Items[i] ? "present" : "null"));
		if (GameLogic.Instance.Items.Length > i && GameLogic.Instance.Items[i])
		{

			GameObject obj = Instantiate(GameLogic.Instance.Items[i]);
			obj.transform.position = position;
			return obj.GetComponent<ItemMain>();
		}
		return null;
	}
}
