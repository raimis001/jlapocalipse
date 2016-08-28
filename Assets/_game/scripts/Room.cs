using UnityEngine;
using UnityEngine.EventSystems;
using System;



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

	private LightSwitch _lightSwitch;
	public LightSwitch lightSwitch;
	public LightSwitch LightSwitch {
		get { return lightSwitch; }
		set {
			if (Lights)
			{
				Lights.SetActive(lightSwitch == LightSwitch.On);
			}
		}
	}

	private bool _selected;
	public bool Selected {
		get { return _selected; }
		set {
			_selected = value;
			SelectedObject.SetActive(_selected);
		}
	}

	private RoomPosition _position;
	public RoomPosition Position;

	internal RoomDevice Device;
	
	[Header("Building")]
	public RoomStatus RoomSatus;
	public GameObject BuildObject;
	public ProgressBar BuildProgress;
	internal float BuildProgressValue;
	public GameObject Decoration;

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

		result.RoomSatus = RoomStatus.Building;
		result.BuildProgress.Value = 1;

		return result;
	}

	public static Room GetRoom(RoomPosition pos)
	{
		return GetRoom(pos.x, pos.y);
	}

	public static Room GetRoom(int x, int y)
	{
		Room[] rooms = FindObjectsOfType<Room>();

		foreach (Room room in rooms)
		{
			if (room.Position.Equals(x, y))
			{
				return room;
			}
		}

		return null;
	}


	public void Awake()
	{
		GameLogic.Rooms.Add(this);
	}

	void Start()
	{
		transform.position = RoomPosition.Vector3(Position);
		SetWalls();

		if (RoomSatus == RoomStatus.Building)
		{
			BuildProgress.Value = 1;
			BuildProgressValue = 1;
			BuildObject.SetActive(true);
			Decoration.SetActive(false);
			lightSwitch = LightSwitch.Off;

		}
	}

	public void OnDestroy()
	{
		GameLogic.Rooms.Remove(this);
	}

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

		if (lightSwitch != _lightSwitch)
		{
			_lightSwitch = lightSwitch;
			LightSwitch = _lightSwitch;
		}

		if (!Application.isPlaying) return;

		if (RoomSatus == RoomStatus.Building)
		{
			SetBuildProgress();
		}
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

	void SetBuildProgress()
	{
		if (BuildProgressValue > 0.2f)
		{
			BuildProgress.Value = (BuildProgressValue - 0.2f) / 0.8f;
			return;
		}

		if (BuildProgressValue < 0.15f)
		{
			lightSwitch = LightSwitch.On;
		}
		if (BuildProgressValue < 0.07f)
		{
			Decoration.SetActive(true);
		}

	}

	public void EndBuild()
	{
		BuildObject.SetActive(false);
		lightSwitch = LightSwitch.On;
		Decoration.SetActive(true);

		RoomSatus = RoomStatus.Ready;
	}

	void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		//Debug.Log("click: " + this);
		GameLogic.SelectedRoom = this;
	}
	public override string ToString()
	{
		return "room @ x:" + Position.x + " y:" + Position.y + " type:" + RoomType;
	}
}
