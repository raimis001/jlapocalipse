using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GameLogic : MonoBehaviour
{

	private static GameLogic _instance;
	public static GameLogic Instance { get { return _instance; } }

	public static List<Room> Rooms = new List<Room>();
	public static CavePathfinder Pathfinder = new CavePathfinder();

	public static void CreateRoom(RoomPosition pos)
	{
		Room room = Room.CreateRoom(pos, RoomType.SERVICE);

		int weight = 0;
		if (pos.HasLeft())
		{
			weight += 1;
			pos.LeftRoom.Weight += 2;
		}
		if (pos.HasRight())
		{
			weight += 2;
			pos.RightRoom.Weight += 1;
		}

		room.Weight = weight;

		Pathfinder.Prepare();
	}

	public delegate void RoomSelect(Room room);
	public static event RoomSelect OnRoomSelect;

	private static Room _selectedRoom;
	public static Room SelectedRoom
	{
		get { return _selectedRoom; }
		set
		{
			if (_selectedRoom == value)
			{
				return;
			}
			if (_selectedRoom)
			{
				_selectedRoom.Selected = false;
				if (!value || _selectedRoom.Position.ToString().Equals(value.Position.ToString()))
				{
					_selectedRoom = null;
					Instance.CloseInterface();
					if (OnRoomSelect != null) OnRoomSelect(null);
					return;
				}
			}

			_selectedRoom = value;
			if (_selectedRoom)
			{
				_selectedRoom.Selected = true;
				Instance.OpenInterface();
			}
			if (OnRoomSelect != null) OnRoomSelect(_selectedRoom);

		}
	}

	public static ItemMain DragItem;

	[Header("Prefabs")]
	public GameObject CaveObject;
	public GameObject RoomPrefab;
	public GameObject BuildPrefab;
	public GameObject BulletPrefab;

	public GameObject[] DevicesPrefab;

	public GameObject[] Items;
	public GameObject[] Foods;

	[Header("Interface")]
	public GameObject Interface;

	[Header("Test")]
	public Text OxigeGen;
	public GridDraw PathDrawer;


	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		Pathfinder.Prepare();
		PathDrawer.DrawGrid();
		CloseInterface();
	}

	void Update()
	{
		Cave.Update();
		OxigeGen.text = Cave.Storage.Oxigen.ToString();

		if (Input.GetMouseButtonUp(1))
		{
			Build.Clear();
			Gui.SelectedObject = null;
		}

	}

	public static BarValues GetValues() {
		
		RoomDevice[] devices = FindObjectsOfType<RoomDevice>();
		BarValues values = new BarValues();

		float eC = 0;
		float eU = 0;
		foreach (RoomDevice device in devices) 
		{

			if (device.Working)
			{
				if (device.Values.Energy > 0)
				{
					eC += device.Values.Energy;
				}
				else
				{
					eU += -device.Values.Energy;
				}
			}

		}

		if (eC < eU) {
			values.Energy = 0;
			
			foreach (RoomDevice device in devices.OrderBy(d => d.Values.Energy)) {
				if (!device.Working || device.Values.Energy > 0) continue;

				device.Working = false;
				eU += device.Values.Energy;

				if (eU <= eC) break;
			}
		}

		values.Energy = 1 - 1 / (eC / eU);


		return values;
	}

	public static Room RoomByPosition(int x, int y) 
	{
		RoomPosition pos = new RoomPosition() { x = x, y = y };
		return RoomByPosition(pos);
	}

	public static Room RoomByPosition(RoomPosition position) 
	{
		Room[] rooms = FindObjectsOfType<Room>();

		foreach (Room room in rooms) 
		{
			if (room.Position.Equals(position)) 
			{
				return room;
			}
		}

		return null;
	}

	public static void CreateBuildLocations()
	{
		Room[] rooms = FindObjectsOfType<Room>();

		List<string> roomList = new List<string>();
		foreach (Room room in rooms)
		{
			roomList.Add(room.Position.hash);
		}

		Build.Clear();
		foreach (Room room in rooms)
		{
			//TODO: look left and right
			string test = RoomPosition.Hash(room.Position.x - 1, room.Position.y);
			if (!roomList.Contains(test))
			{
				Build.Create(room.Position.x - 1, room.Position.y);
			}
			test = RoomPosition.Hash(room.Position.x + 1, room.Position.y);
			if (!roomList.Contains(test))
			{
				Build.Create(room.Position.x + 1, room.Position.y);
			}
		}


	}

#region Prefabs

	public static GameObject GetFood(RawFood food)
	{
		if (!Instance) return null;

		int f = (int) food;
		if (Instance.Foods.Length < 1 || Instance.Foods.Length <= f) return null;

		return Instance.Foods[f];
	}
	#endregion

#region Interface
	public void OpenInterface()
	{
		if (Interface)
		{
			Interface.SetActive(true);
			Interface.transform.SetParent(SelectedRoom.transform);
			Interface.transform.localPosition = Vector3.zero;
		}
	}
	public void CloseInterface()
	{
		if (Interface) Interface.SetActive(false);
	}

	public void OnCloseRoom()
	{
		SelectedRoom = null;
	}
#endregion
}
