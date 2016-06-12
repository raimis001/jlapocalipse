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

}

