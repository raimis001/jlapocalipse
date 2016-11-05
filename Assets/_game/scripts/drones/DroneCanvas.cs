using UnityEngine;


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

	public override void BuildRoom(Room room)
	{
		DroneMover drone = GetComponent<DroneMover>();
		if (!drone)
		{
			return;
		}
		drone.MoveToRoom(room);
	}

	public override void ButtonClick(string tag)
	{
		DroneMover drone = GetComponent<DroneMover>();
		if (!drone)
		{
			return;
		}

		Debug.Log("Button click:" + tag);
		if (tag.Equals("buildRoom"))
		{
			GameLogic.CreateBuildLocations();
			
		}
		else if (tag.Equals("droneCharge"))
		{
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
