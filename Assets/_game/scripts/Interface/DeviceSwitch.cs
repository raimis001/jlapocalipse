using UnityEngine;
using System.Collections;

public class DeviceSwitch : MonoBehaviour
{

	public RoomDevice Device;

	void OnMouseUp()
	{
		Debug.Log("Power click");
		if (!Device)
		{
			return;
		}

		Device.Working = !Device.Working;
	}
}
