using UnityEngine;
using System.Collections;
using Engine.Pathfinder;
using System.Collections.Generic;

public class DroneMover : MonoBehaviour
{
	public Vector3[] RoomPoints;
	public Room CurrentRoom;
	public GameObject EffectWork;

	private Room TargetRoom;

	public Transform Body;
	public Transform Droid;

	private bool _moving;
	private bool _break;

	public void OnDisable()
	{
		GameLogic.OnRoomSelect -= OnRoomSelect;
	}

	public void OnEnable()
	{
		GameLogic.OnRoomSelect += OnRoomSelect;
	}
	public void Start()
	{
		/*
		if (RoomPoints.Length > 0) 
		{
			Droid.localPosition = RoomPoints[0];
			StartCoroutine(Move());
		}
		*/

		transform.SetParent(CurrentRoom.transform);
		transform.localPosition = Vector3.zero;

		//StartCoroutine(MoveToRoom());

	}

	void OnRoomSelect(Room room) 
	{
		
		if (room == null) 
		{
			TargetRoom = null;
			return;
		}

		TargetRoom = room;

		//TODO find path and move
		//Debug.Log("Finding path");
		EffectWork.SetActive(false);
		List<GridNode> path = GameLogic.Pathfinder.FindPath(CurrentRoom.Position, TargetRoom.Position);
		
		GameLogic.Instance.PathDrawer.FillPath(path);
		
		StartCoroutine(MoveByPath(path));
	}

	void StartWorking() 
	{
		EffectWork.SetActive(true);
	}
	bool elevator = false;
	IEnumerator MoveByPath(List<GridNode> path) 
	{
		if (path.Count < 1)
		{
			//Debug.Log("No path");
			yield break;
		}

		if (Vector3.Distance(transform.position, RoomPoints[1]) > 0.5) 
		{
			//Debug.Log("Goto begin point");
			yield return Move(RoomPoints[1], 1);
		}

		foreach (GridNode node in path) 
		{
			if (!node.reference) 
			{
				//Go to elevator
				Debug.Log("goto elevator");
				if (!elevator)
				{
					yield return MoveToElevator();
				}
				continue;
			}
			Room next = node.reference.GetComponent<Room>();
			int direction;
			if (next.Position.y != CurrentRoom.Position.y)
			{
				//Go up or down
				Debug.Log("Need to climb or down");

				direction = next.Position.y < CurrentRoom.Position.y ? 0 : 1;

				if (direction == 0)
				{
					Debug.Log("climb UP");
					yield return Move(RoomPoints[6], 2);
				}

				Debug.Log("set new room");
				CurrentRoom = next;
				transform.SetParent(CurrentRoom.transform);
				transform.localPosition = Vector3.zero;

				if (direction == 0)
				{
					Debug.Log("set new position after climb");
					Droid.transform.localPosition = RoomPoints[4];
				}
				else 
				{
					Debug.Log("Clib down");
					Droid.transform.localPosition = RoomPoints[6];
					yield return Move(RoomPoints[4], 1);
					elevator = true;
				}

				continue;
			}
			if (elevator) 
			{
				Debug.Log("Move from stairs");
				yield return Move(RoomPoints[5], 1);
				elevator = false;
			}

			direction = next.Position.x < CurrentRoom.Position.x ? 0 : 1;

			Vector3 fStep = direction == 0 ? RoomPoints[3] : RoomPoints[2];
			
			//Debug.Log("Goto first randevu");
			yield return Move(fStep, 2);

			CurrentRoom = next;
			transform.SetParent(CurrentRoom.transform);
			transform.localPosition = Vector3.zero;
			Droid.transform.localPosition = direction == 0 ? RoomPoints[2] : RoomPoints[3];
			//yield break;
		}

		yield return Move(RoomPoints[1], 1);
		yield return Move(RoomPoints[0], 1);
		StartWorking();
	}

	IEnumerator MoveToElevator() 
	{
		yield return Move(RoomPoints[5], 1);
		yield return Move(RoomPoints[4], 1);
	}

	IEnumerator Move(Vector3 positionTo, float time)
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

		float delta = Vector3.Distance(positionFrom, positionTo) / 1.5f;
		//Debug.Log(delta);
		time = delta;

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

	public void OnMouseUp()
	{
		Debug.Log("Click on droid");
	}
}
