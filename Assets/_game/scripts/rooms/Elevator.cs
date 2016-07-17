using UnityEngine;
using System.Collections;

public enum ElevatorDirection
{
	Up,
	Down
}

public class Elevator : MonoBehaviour
{

	public float Speed = 3f;
	public Transform Passenger;
	public Transform PassengerPoint;
	public Room[] Rooms;

	[Header("Interface")] 
	public Button ButtonUp;
	public Button ButtonDown;
	public Button ButtonGo;

	public DigitsPanel PanelFloor;

	//private Transform _passengerParent;

	private bool _working;
	private int _floor;
	private int _targetFloor;

	public void Start()
	{
		_floor = Rooms.Length - 1;
		_targetFloor = _floor;
		transform.SetParent(Rooms[_floor].transform);
		transform.localPosition = Vector3.zero;

		if (Passenger)
		{
			Passenger.transform.SetParent(PassengerPoint);
			Passenger.transform.localPosition = Vector3.zero;
		}

		if (ButtonDown) ButtonDown.OnButton = FloorDown;
		if (ButtonUp) ButtonUp.OnButton = FloorUp;
		if (ButtonGo) ButtonGo.OnButton = StartElevator;
	}

	public void OnMouseUp()
	{
		//Debug.Log("MUp");
		if (!_working)
		{
			//StartCoroutine(GotoFloor(_floor == 0 ? Rooms.Length - 1 : 0));
		}
	}

	public void OnMouseDown()
	{
		//Debug.Log("MDown");
	}

	public void FloorUp()
	{
		if (_targetFloor > 0) _targetFloor--;
		if (PanelFloor) PanelFloor.Number = _targetFloor;
	}
	public void FloorDown()
	{
		if (_targetFloor < Rooms.Length - 1) _targetFloor++;
		if (PanelFloor) PanelFloor.Number = _targetFloor;
	}

	public void SetFloor(int floor)
	{
		_targetFloor = Mathf.Clamp(floor, 0, Rooms.Length -1);
		if (PanelFloor) PanelFloor.Number = _targetFloor;
	}

	public void StartElevator()
	{
		if (!_working)
		{
			StartCoroutine(GotoFloor(_targetFloor));
		}
	}

	#region WORK
	IEnumerator Work(float from, float to)
	{
		float time = Speed;
		while (time > 0)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(from, to, 1f - time / Speed), transform.localPosition.z);
			time -= Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator GoTo(ElevatorDirection dir)
	{
		if (dir == ElevatorDirection.Up)
		{
			yield return GoToUp();
		}
		else
		{
			yield return GoToDown();
		}
	}

	IEnumerator GoToUp()
	{
		yield return Work(0, 8);

		_floor -= 1;

		transform.SetParent(Rooms[_floor].transform);
		transform.localPosition = Vector3.zero;
	}
	
	IEnumerator GoToDown()
	{
		transform.SetParent(Rooms[_floor+1].transform);
		transform.localPosition = new Vector3(0,8f,0);

		yield return Work(8, 0);

		_floor += 1;
		transform.SetParent(Rooms[_floor].transform);
		transform.localPosition = Vector3.zero;
	}

	IEnumerator GotoFloor(int floor)
	{
		if (_working) yield break;
		if (floor == _floor) yield break;
		_working = true;

		ElevatorDirection dir = floor < _floor ? ElevatorDirection.Up : ElevatorDirection.Down;
		Debug.Log("Elevator to floor:" + floor + " direction:" + dir);

		while (floor != _floor)
		{
			yield return GoTo(dir);
		}


		_working = false;
	}
	#endregion

	public void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Passenger arrive");
		//if (Passenger)
		//{
		//	_passengerParent = Passenger.parent;
		//	Passenger.SetParent(transform);
		//}
	}

	public void OnTriggerExit(Collider other)
	{
		//Debug.Log("Passenger left elevator");
		//Passenger.SetParent(_passengerParent);

	}
}
