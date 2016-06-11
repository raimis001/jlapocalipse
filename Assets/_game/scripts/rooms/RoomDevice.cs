using UnityEngine;
using System.Collections;

public class RoomDevice : MonoBehaviour
{

	public static RoomDevice Create(RoomType type, Transform parent, RoomPosition pos)
	{
		GameObject obj = Instantiate(GameLogic.Device(type)) as GameObject;
		obj.transform.SetParent(parent);
		obj.transform.localPosition = Vector3.zero;

		RoomDevice device = obj.GetComponent<RoomDevice>();
		device.Room = pos;

		return device;
	}

	public RoomPosition Room;

	public Progress3D Damage;
	public Progress3D Output;

	public Progress3D OxigenStorage;


	public Light Power;

	void Update()
	{
		if (OxigenStorage)
		{
			OxigenStorage.Value = Room.Property.Storage.Oxigen;
		} 
	}
}
