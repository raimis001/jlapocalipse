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
		DisableAll();
		if (!GameLogic.SelectedRoom) return;
	}

	void OnDisable()
	{
		//Debug.Log("Disable selected");
	}
}
