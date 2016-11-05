using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

	public Transform Body;

	[Header("Progress bars")]
	public ProgressBar HealthBar;
	public ProgressBar HungryBar;
	public ProgressBar TiredBar;

	[Header("Path")]
	public raWaypointPath Path;

	private bool _moving = false;
	private bool _break = false;

	private int _currentWayPoint;
	private Room _currentRoom;

	//public Inventory Inventory;

	// Use this for initialization
	void Start()
	{
		//Inventory.gameObject.SetActive(false);

		HealthBar.Value = 1;
		HungryBar.Value = 1;
		TiredBar.Value = 1;

		_currentWayPoint = 0;

		Body.localPosition = Path.WaypointList[_currentWayPoint].transform.localPosition;
		Body.eulerAngles = Path.WaypointList[_currentWayPoint].transform.eulerAngles;


		_currentRoom = GameLogic.GetRoom(0, 0);
		transform.SetParent(_currentRoom.transform);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonUp(1))
		{
			//ItemMain item = ItemMain.Create(Helper.GetRandomEnum<ItemKind>());
			//if (!Inventory.AddItem(item))
			{
				//Destroy(item.gameObject);
			}
		}
	}

	public void GotoSelected()
	{
		if (GameLogic.SelectedRoom != null)
		{
			//transform.SetParent(GameLogic.SelectedRoom.transform);
			//transform.localPosition = Vector3.zero;
			//_currentRoom = GameLogic.SelectedRoom;
			StartCoroutine(Move(GameLogic.SelectedRoom));
		}
	}

	public void OnMouseUp()
	{
		Debug.Log("Mose press on character");
		//Inventory.gameObject.SetActive(!Inventory.gameObject.activeSelf);
		if (_moving) return;

		StartCoroutine(Move(_currentWayPoint == 0 ? 2 : 0));
	}

	IEnumerator Move(int waypoint)
	{
		yield return Rotate(_currentWayPoint < waypoint ? new Quaternion(0, -180, 0, 0) : Quaternion.identity);

		int wpoint = _currentWayPoint < waypoint ? 0 : 2;
		while (wpoint != waypoint)
		{
			wpoint += _currentWayPoint < waypoint ? 1 : -1;
			Debug.Log("Waypoint:" + wpoint);
			yield return Move(Path.WaypointList[wpoint]);
		}

		_currentWayPoint = waypoint;
		yield return Rotate(Path.WaypointList[_currentWayPoint].transform.rotation);
	}

	IEnumerator Move(raWaypoint waypoint)
	{
		yield return Move(waypoint.transform.localPosition);
	}

	IEnumerator Move(Vector3 positionTo)
	{
		if (_moving)
		{
			yield break;
		}
		_moving = true;
		Vector3 positionFrom = Body.localPosition;
		float deltaTime = 0;

		//float angle = Helper.AngleInDeg(positionFrom, positionTo) + 0f;
		//StartCoroutine(RotateBody(angle));

		float delta = Vector3.Distance(positionFrom, positionTo) / 2.5f;
		float time = delta;

		while (deltaTime < time)
		{
			Body.transform.localPosition = Vector3.Lerp(positionFrom, positionTo, deltaTime / time);
			deltaTime += Time.smoothDeltaTime;
			if (_break)
			{
				_break = false;
				_moving = false;
				yield break;
			}

			yield return null;
		}
		
		Body.transform.localPosition = positionTo;
		_moving = false;
	}

	IEnumerator MoveToRoom(Room room)
	{
		yield return Move(room);
	}
	IEnumerator Move(Room room)
	{
		if (_currentRoom.Position.x < room.Position.x)
		{
			//Goto right
			if (_currentWayPoint !=2)
			{
				_currentWayPoint = 2;
				yield return Move(Path.WaypointList[_currentWayPoint]);
			}
			if (_currentWayPoint != 4)
			{
				_currentWayPoint = 4;
				yield return Move(Path.WaypointList[_currentWayPoint]);
				_currentWayPoint = 0;
			}
		}
		else
		{
			//Goto left
			if (_currentWayPoint != 0)
			{
				_currentWayPoint = 0;
				yield return Move(Path.WaypointList[_currentWayPoint]);
			}
			if (_currentWayPoint != 5)
			{
				_currentWayPoint = 5;
				yield return Move(Path.WaypointList[_currentWayPoint]);
				_currentWayPoint = 2;
			}

		}
		_currentRoom = room;
		transform.SetParent(_currentRoom.transform);
		transform.localPosition = Vector3.zero;
		Body.transform.localPosition = Path.WaypointList[_currentWayPoint].transform.localPosition;
	}
	IEnumerator Rotate(Quaternion rotationTo)
	{
		float maxTime = 0.3f;
		float time = 0;
		Quaternion start = Body.rotation;
		while (time < maxTime)
		{
			Body.transform.rotation =  Quaternion.Lerp(start, rotationTo, time/maxTime);

			time += Time.smoothDeltaTime;
			yield return null;
		}
	}
}
