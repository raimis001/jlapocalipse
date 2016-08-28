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
		GameLogic.CreateBuildLocations();
	}
}
