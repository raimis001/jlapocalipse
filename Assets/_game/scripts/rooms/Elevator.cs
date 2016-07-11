using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{

	public Transform Passenger;

	private Transform _passengerParent;

	private bool _working;

	public void Update()
	{
	}

	public void OnMouseUp()
	{
		Debug.Log("MUp");
		if (!_working)
		{
			StartCoroutine(Work());
		}
	}

	public void OnMouseDown()
	{
		Debug.Log("MDown");
	}

	IEnumerator Work()
	{
		if (_working) yield break;

		_working = true;

		float maxTime = 3f;
		float time = maxTime;
		while (time > 0)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(0,8f,1f - time / maxTime), transform.localPosition.z);
			time -= Time.deltaTime;
			yield return null;
		}
		_working = false;
	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("Passenger arrive");
		if (Passenger)
		{
			_passengerParent = Passenger.parent;
			Passenger.SetParent(transform);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		Debug.Log("Passenger left elevator");
		//Passenger.SetParent(_passengerParent);

	}
}
