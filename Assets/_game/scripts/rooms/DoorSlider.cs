using UnityEngine;
using System.Collections;

public class DoorSlider : MonoBehaviour
{


	public iTweenEvent OpenEvent;
	public iTweenEvent CloseEvent;

	private bool _opened;
	public bool Opened
	{
		get { return _opened; }
		set
		{
			_opened = value;

			if (_opened)
			{
				//CloseEvent.Stop();
				OpenEvent.Play();
			}
			else
			{
				//OpenEvent.Stop();
				CloseEvent.Play();
			}
			

		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		Opened = !_opened;
	}
}
