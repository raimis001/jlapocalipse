using UnityEngine;
using System.Collections;

public class guiSelectedRoom : MonoBehaviour
{

	public GameObject[] Rooms;


	void DisableAll()
	{
		foreach (GameObject obj in Rooms)
		{
			obj.SetActive(false);
		}
	}

	void OnEnable()
	{
		Debug.Log("Set select room");
		DisableAll();
		if (!GameLogic.SelectedRoom) return;

		Rooms[(int)GameLogic.SelectedRoom.Type].SetActive(true);
	}

	void OnDisable()
	{
		Debug.Log("Disable selected");
	}
}
