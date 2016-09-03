using UnityEngine;
using System.Collections;

public class DeviceCharger : RoomDevice
{
	public raWaypoint Waypoint;

	public SwithLight Indicator;

	internal bool isFree = true;
	internal DroneMover Drone;


	public void OnTriggerStay(Collider other)
	{
		DroneMover drone = other.gameObject.GetComponentInParent<DroneMover>();
		if (drone == Drone)
		{
			//Debug.Log("Trigger load:" + other.gameObject.name);
			Drone d = drone.gameObject.GetComponent<Drone>();
			if (d)
			{
				d.DoCharge = true;
				if (Indicator) Indicator.Status = d.Energy < 1 ? LightSwitch.On : LightSwitch.Off;
			}

		}
	}
}
