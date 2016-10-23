using UnityEngine;
using System.Collections;
using Engine.Pathfinder;
using System.Collections.Generic;

public enum DroneStatus
{
	None,
	Move,
	Working
}

public class DroneMover : MonoBehaviour
{
	public Room CurrentRoom;
	public GameObject EffectWork;
	public raWaypointPath Waypoints;

	private Room TargetRoom;

	public Transform Body;
	public Transform Droid;

	private bool _moving;
	private bool _break;
	private int _waypoint;

	public DroneStatus Status;
	
	public void OnDisable()
	{
		//GameLogic.OnRoomSelect -= OnRoomSelect;
	}

	public void OnEnable()
	{
		//GameLogic.OnRoomSelect += OnRoomSelect;
	}

	public void Start()
	{
		SetCurrentRoom(CurrentRoom,0);
	}

	public void MoveToRoom(Room room)
	{
		List<GridNode> path = GameLogic.Pathfinder.FindPath(CurrentRoom.Position, room.Position);
		if (path.Count < 1)
		{
			return;
		}
		GameLogic.Instance.PathDrawer.FillPath(path);

		StartCoroutine(MoveByPath(path));
	}

	public void MoveToCharger(DeviceCharger charger)
	{
		StartCoroutine(IEMoveToCharger(charger));
	}

	void SetCurrentRoom(Room room, int waypoint)
	{
		CurrentRoom = room;
		transform.SetParent(CurrentRoom.transform);
		transform.localPosition = Vector3.zero;

		Droid.transform.localPosition = Waypoints.localPosition(waypoint);
		_waypoint = waypoint;
	}

	void OnRoomSelect(Room room)
	{
		if (!room || room == CurrentRoom || Status != DroneStatus.None)
		{
			return;
		}
		MoveToRoom(room);
	}

#region WORKING
	void StartWorking(Room work)
	{
		StartCoroutine(DoWorking(work));
	}

	IEnumerator DoWorking(Room work)
	{
		EffectWork.SetActive(true);
		Status = DroneStatus.Working;

		while (work.BuildProgressValue > 0)
		{
			yield return null;
			work.BuildProgressValue -= Time.smoothDeltaTime / 3f;
		}
		work.EndBuild();

		EffectWork.SetActive(false);

		yield return IEMoveToRoom(work);
		yield return Move(1);

		Status = DroneStatus.None;


	}

	IEnumerator StopWorking()
	{
		EffectWork.SetActive(false);
		if (Status != DroneStatus.Working)
		{
			yield break;
		}

		yield return Move(0);
		Status = DroneStatus.None;
	}
	#endregion

#region MOVE INTERFACE
	IEnumerator IEMoveToCharger(DeviceCharger charger)
	{
		Room room = charger.ParentRoom;
		List<GridNode> path = GameLogic.Pathfinder.FindPath(CurrentRoom.Position, room.Position);

		yield return MoveByPath(path);

		yield return Move(charger.Waypoint);
	}

	IEnumerator IEMoveToStreet()
	{
		if (_waypoint > 0)
		{
			yield break;
		}

		yield return Move(1);

	}

	IEnumerator IEMoveToRoom(Room next)
	{
	
		yield return IEMoveToStreet();

		if (next.Position.y != CurrentRoom.Position.y)
		{
			//Going up or down
			if (next.Position.y < CurrentRoom.Position.y)
			{
				//Going up
				yield return Move(8);
				SetCurrentRoom(next, 7);
			}
			else
			{
				SetCurrentRoom(next, 8);
				yield return Move(7);
			}
			yield break;
		}

		if (_waypoint == 7)
		{
			yield return Move(6);
		}


		int direction = next.Position.x < CurrentRoom.Position.x ? 2 : 3;

		yield return Move(direction);

		if (next.RoomSatus == RoomStatus.Building)
		{
			Debug.Log("This room is need to build");
			StartWorking(next);
			yield break;
		}

		direction = direction == 2 ? 5 : 4;
		SetCurrentRoom(next, direction);

		direction = direction == 5 ? 3 : 2;
		yield return Move(direction);

	}

	IEnumerator MoveToElevator()
	{
		if (_waypoint == 7)
		{
			yield break;
		}

		if (_waypoint != 6)
		{
			yield return Move(6);
		}

		yield return Move(7);
	}

	IEnumerator MoveByPath(List<GridNode> path)
	{
		Status = DroneStatus.Move;
		yield return StopWorking();

		foreach (GridNode node in path)
		{
			if (!node.reference)
			{
				yield return MoveToElevator();
				continue;
			}

			Room next = node.reference.GetComponent<Room>();
			yield return IEMoveToRoom(next);
		}

		if (_waypoint == 7)
		{
			yield return Move(6);
		}

		if (Status == DroneStatus.Working)
		{
			yield break;
		}

		yield return Move(1);
		Status = DroneStatus.None;
	}
#endregion

#region MOVE/ROTATE
	IEnumerator Move(raWaypoint waypoint)
	{
		yield return Move(waypoint.transform.localPosition);
	}

	IEnumerator Move(int waypoint)
	{
		yield return Move(Waypoints.localPosition(waypoint));
		_waypoint = waypoint;
	}

	IEnumerator Move(Vector3 positionTo)
	{
		if (_moving)
		{
			yield break;
		}
		_moving = true;
		Vector3 positionFrom = Droid.localPosition;
		float deltaTime = 0;

		if (Body)
		{
			float angle = Helper.AngleInDeg(positionFrom, positionTo) + 0f;
			StartCoroutine(RotateBody(angle));
		}

		float delta = Vector3.Distance(positionFrom, positionTo) / 2.5f;
		//Debug.Log(delta);
		float time = delta;

		while (deltaTime < time)
		{
			Droid.transform.localPosition = Vector3.Lerp(positionFrom, positionTo, deltaTime / time);


			deltaTime += Time.smoothDeltaTime;
			if (_break)
			{
				_break = false;
				_moving = false;
				yield break;
			}

			yield return null;
		}

		Droid.transform.localPosition = positionTo;
		_moving = false;
	}

	IEnumerator RotateBody(float angle)
	{
		float time = 0.5f;
		float deltaTime = 0;
		float startAngle = Body.localEulerAngles.y;

		if (startAngle - angle > 180) startAngle -= 360;
		if (startAngle - angle < -180) startAngle += 360;

		//Debug.Log("Rotate from:" + startAngle + " to:" + angle);

		while (deltaTime < time)
		{

			//Body.localEulerAngles = new Vector3(0, Mathf.Lerp(startAngle,angle, deltaTime / time), 0);

			Body.localRotation = Quaternion.Euler(0, Mathf.Lerp(startAngle, angle, deltaTime / time), 0);

			deltaTime += Time.deltaTime;

			yield return null;
		}

		Body.localEulerAngles = new Vector3(0, angle, 0);

	}
#endregion

#region CHARGE



#endregion

}
