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

	public void ResetPosition()
	{
		//transform.localPosition = Inventory.Position();
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
		if (!Enabled) return;
		Debug.Log(" mouse up Item:" + ItemKind);
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//RaycastHit[] hits = Physics.RaycastAll(ray);
		/*
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
		*/
		ResetPosition();
	}


	public void OnMouseDrag()
	{
		if (!Enabled) return;

		float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		float z = transform.position.z;

		/*
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
		*/
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
		transform.position = new Vector3(pos_move.x - 0.25f, pos_move.y - 0.0f, z);

	}

	public static ItemMain Create(ItemKind kind)
	{
		if (!GameLogic.Instance) return null;
		int i = (int) kind;
		//Debug.Log("Get kind:" + kind + " int:" + i + " len:" + GameLogic.Instance.Items.Length + " prefab:" + (GameLogic.Instance.Items[i] ? "present" : "null"));
		if (GameLogic.Instance.Items.Length > i && GameLogic.Instance.Items[i])
		{

			GameObject obj = Instantiate(GameLogic.Instance.Items[i]);
			return obj.GetComponent<ItemMain>();
		}
		return null;
	}
}
