using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RoomDevice : MonoBehaviour
{

	private RoomPosition _position;
	public RoomPosition Position;
	public Light Power;

	private bool _lightsOn = true;
	public bool LightsOn {
		get { return _lightsOn; }
		set {
			if (_lightsOn == value) return;

			_lightsOn = value;
			if (Power)
			{
				Power.gameObject.SetActive(_lightsOn);
			}
		}
	}

	private bool _working;
	public bool Working = true;
	private bool working 
	{
		set 
		{
			LightsOn = value;
		}
	}

	public BarValues Values;

	internal Room ParentRoom
	{
		get
		{
			return GetComponentInParent<Room>();
		}
	}

	protected virtual void Start()
	{
		//if (Inventory) Inventory.OnChange = InventoryChanged;

		Room room = GetComponentInParent<Room>();
		if (room)
		{
			Debug.Log("Device " + gameObject.name + " in room:" + room.gameObject.name);
		}
	}

	protected virtual void Update()
	{
		if (Working != _working) 
		{
			_working = Working;
			working = Working; 	
		}

		if (!Position.Equals(_position)) 
		{
			_position.x = Position.x;
			_position.y = Position.y;
			transform.position = _position.Position;
		}
	}

	public virtual void EndDrag(ItemMain item)
	{
		Debug.Log("End dragging item:" + item.ItemKind);
		/*
		if (!Inventory)
		{
			item.ResetPosition();
			return;
		}

		if (!Inventory.AddItem(item))
		{
			item.ResetPosition();
		}
		*/
	}

	private void InventoryChanged()
	{
		OnInventoryChange();
	}

	protected virtual void OnInventoryChange()
	{

	}

}

