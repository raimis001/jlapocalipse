using UnityEngine;
using System.Collections;

public class DoorRoom : MonoBehaviour
{

	public DoorSlider Slider;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnTriggerEnter(Collider other)
	{

		//Debug.Log("enter door area:" + other.name);
		if (!Slider) return;
		if (Slider.Opened) return;

		Slider.Opened = true;
	}

	public void OnTriggerStay(Collider other)
	{

	}

	public void OnTriggerExit(Collider other)
	{
		//Debug.Log("exit door area:" + other.name);
		if (!Slider) return;
		if (!Slider.Opened) return;

		Slider.Opened = false;

	}
}
