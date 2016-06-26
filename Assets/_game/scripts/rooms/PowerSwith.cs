using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class PowerSwith : MonoBehaviour
{

	public UnityEvent ButtonPress;


	void OnMouseUp() 
	{
		ButtonPress.Invoke();
	}
}
