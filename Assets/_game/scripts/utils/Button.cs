using System;
using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

	public Action OnButton;

	public void OnMouseUpAsButton()
	{
		Debug.Log("Botton pressed");
		if (OnButton != null)
		{
			OnButton();
		}
	}
}
