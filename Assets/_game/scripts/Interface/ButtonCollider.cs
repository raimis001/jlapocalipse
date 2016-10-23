using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ButtonCollider : MonoBehaviour
{

	public UnityEvent OnClick;

	public void OnMouseDown()
	{
		Debug.Log("On button click");
		if (OnClick != null)
		{
			OnClick.Invoke();
		}
	}
}
