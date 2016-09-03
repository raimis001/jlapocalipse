using UnityEngine;
using System.Collections;

public class SwithLight : MonoBehaviour
{
	public LightSwitch Status = LightSwitch.Off;

	public GameObject Light;
	public Renderer LightObject;

	public Material MaterialOff;
	public Material MaterialOn;

	private LightSwitch _status = LightSwitch.On;

	public void Update()
	{
		if (_status != Status)
		{
			if (Light) Light.SetActive(Status == LightSwitch.On);
			if (LightObject) LightObject.material = Status == LightSwitch.On ? MaterialOn : MaterialOff;
			_status = Status;
		}
	}
}
