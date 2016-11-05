using UnityEngine;
using System.Collections;

public class DoorDevice : MonoBehaviour
{

	public Animator doorAnim;

	internal bool doorOpened;

	public void OnMouseDown()
	{
		doorAnim.SetTrigger(doorOpened ? "Close" : "Open");
		doorOpened = !doorOpened;
			 
	}
}
