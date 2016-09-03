using UnityEngine;
using System.Collections;

public class DroneCanvas : CanvasObject
{
	public override void Deselect()
	{
		Debug.Log("Drone deselect");
	}

	public override void Select()
	{
		Debug.Log("Drone select");
		Gui.OpenMenu(1);
	}

	public override void ButtonClick(string tag)
	{
		Debug.Log("Button click:" + tag);
		if (tag.Equals("buildRoom"))
		{
			GameLogic.CreateBuildLocations();
		}
		else if (tag.Equals("droneCharge"))
		{
			DroneMover drone = GetComponent<DroneMover>();
			if (!drone)
			{
				return;
			}

			DeviceCharger[] chargers = FindObjectsOfType<DeviceCharger>();
			foreach (DeviceCharger charger in chargers)
			{
				if (!charger.isFree || !charger.ParentRoom) continue;

				charger.isFree = false;
				charger.Drone = drone;

				drone.MoveToCharger(charger);
				break;
			}
			Debug.Log("no free chargers");

		}
	}
}
