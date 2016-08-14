using UnityEngine;
using UnityEngine.EventSystems;
using System;

[Serializable]
public struct RoomPosition
{
	public int x;
	public int y;

	public RoomPosition Left {
		get { return new RoomPosition() { x = x - 1, y = y }; }
	}
	public RoomPosition Right {
		get { return new RoomPosition() { x = x + 1, y = y }; }
	}

	public Room LeftRoom {
		get { return GameLogic.RoomByPosition(Left); }
	}
	public Room RightRoom {
		get { return GameLogic.RoomByPosition(Right); }
	}

	public bool HasLeft()
	{
		return GameLogic.RoomByPosition(Left) != null;
	}
	public bool HasRight()
	{
		return GameLogic.RoomByPosition(Right) != null;
	}

	public string hash {
		get { return string.Format("{0}:{1}", x, y); }
	}

	public override string ToString()
	{
		return hash;
	}

	public override bool Equals(object obj)
	{
		return ((RoomPosition)obj).hash.Equals(hash);
	}
	public override int GetHashCode()
	{
		return 0;
	}

	public Vector3 Position 
	{
		get {
			return Vector3(this);
		}
	}

	#region STATIC
	public static Vector3 Vector3(RoomPosition pos)
	{
		return new Vector3(pos.x * 10, pos.y * -8, 0);
	}
	#endregion
}


[ExecuteInEditMode]
public class Room : MonoBehaviour
{

	public GameObject SelectedObject;
	public GameObject[] Walls;

	private int _weight;
	[Range(0, 3)]
	public int Weight;

	public GameObject Lights;
	public BarDevice Bar;

	private RoomType _roomType;
	public RoomType RoomType;

	private RoomType _type;
	public RoomType Type {
		get { return _type; }
		set {
			_type = value;

			if (Device)
			{
				Destroy(Device.gameObject);
			}
		
			if (_type == RoomType.NONE) return;

			Device = RoomDevice.Create(_type, transform, Position);
			Device.LightsOn = _lightsOn;
		}
	}

	private bool _lightsOn = true;
	public bool LightsOn
	{
		get { return _lightsOn; }
		set
		{
			//if (_lightsOn == value) return;

			_lightsOn = value;
			if (Lights)
			{
				Lights.SetActive(_lightsOn);
			}
			if (Device)
			{
				Device.LightsOn = _lightsOn;
			}

		}
	}

	private bool _selected;
	public bool Selected
	{
		get { return _selected; }
		set
		{
			_selected = value;
			SelectedObject.SetActive(_selected);
		}
	}

	private RoomPosition _position;
	public RoomPosition Position;

	internal RoomDevice Device;

	public static Room CreateRoom(RoomPosition pos, RoomType type)
	{
		return CreateRoom(pos.x, pos.y, type);
	}
	public static Room CreateRoom(int x, int y, RoomType type)
	{
		GameObject obj = Instantiate(GameLogic.Instance.RoomPrefab);
		obj.transform.SetParent(GameLogic.Instance.CaveObject.transform);

		Room result = obj.GetComponent<Room>();

		result.Position.x = x;
		result.Position.y = y;
		result.Type = type;


		return result;
	}

	// Use this for initialization
	void Start()
	{
		transform.position = RoomPosition.Vector3(Position);
		SetWalls();
	}

	// Update is called once per frame
	void Update()
	{
		if (Weight != _weight || _roomType != RoomType || !_position.Equals(Position))
		{
			_weight = Weight;
			_roomType = RoomType;
			_position.x = Position.x;
			_position.y = Position.y;
			SetWalls();
		}

		if (!Application.isPlaying) return;



	}

	void SetWalls()
	{
		//0 - apkārt nav istabu
		//1 - pa kreisi ir istaba
		//2 - pa labi istaba
		//3 - abās pusēs istaba

		foreach (GameObject wall in Walls)
		{
			wall.SetActive(false);
		}

		switch (_weight)
		{
			case 0:
				Walls[0].SetActive(true);
				Walls[2].SetActive(true);
			break;
			case 1:
				Walls[1].SetActive(true);
				Walls[2].SetActive(true);
				break;
			case 2:
				Walls[0].SetActive(true);
				break;
			case 3:
				Walls[1].SetActive(true);
			break;
		}

		Walls[3].SetActive(RoomType != RoomType.ELEVATOR);
		Walls[4].SetActive(RoomType != RoomType.ELEVATOR);
		Walls[5].SetActive(RoomType == RoomType.ELEVATOR);

		transform.position = RoomPosition.Vector3(Position);

		CaveDraw.LastRoomCount = 0;
	}

	void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		Debug.Log("room click at:" + Position);
		GameLogic.SelectedRoom = this;
	}

}
