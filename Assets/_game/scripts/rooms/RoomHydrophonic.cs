using UnityEngine;
using System.Collections;

public class RoomHydrophonic : RoomDevice
{

	public Vaga[] Vagas;

	public void OnMouseUp()
	{
		Debug.Log("Hidrophonic accepted");
	}

	public void OnMouseUpAsButton()
	{
		Debug.Log("Hidrophonic accepted");
	}
}
