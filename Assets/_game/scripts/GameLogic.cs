using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GameLogic : MonoBehaviour
{

	private static GameLogic _instance;
	public static GameLogic Instance { get { return _instance; } }

	//public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();
	public static List<Build> Places = new List<Build>();

	public static GameObject Device(RoomType type)
	{
		return Instance.DevicesPrefab[(int)type];
	}
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

		ClearPlaces();
	}
	private static void ClearPlaces()
	{
		foreach (Build build in Places)
		{
			Destroy(build.gameObject);
		}

		Places.Clear();
	}

	private static Room _selectedRoom;
	public static Room SelectedRoom
	{
		get { return _selectedRoom; }
		set
		{
			if (_selectedRoom)
			{
				_selectedRoom.Selected = false;
				if (!value || _selectedRoom.Position.ToString().Equals(value.Position.ToString()))
				{
					_selectedRoom = null;
					Instance.BuildMenu.SetActive(true);
					Instance.RoomMenu.SetActive(false);
					return;
				}
			}

			_selectedRoom = value;
			if (_selectedRoom)
			{
				_selectedRoom.Selected = true;
			}
			Instance.BuildMenu.SetActive(!_selectedRoom);
			Instance.RoomMenu.SetActive(_selectedRoom);
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

	[Header("Interface")]
	public GameObject BuildMenu;
	public GameObject RoomMenu;

	[Header("Test")]
	public Text OxigeGen;

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		//CreateRoom(new RoomPosition());
		BuildMenu.SetActive(true);
		RoomMenu.SetActive(false);
	}

	void Update()
	{
		Cave.Update();
		OxigeGen.text = Cave.Storage.Oxigen.ToString();
	}

	public void MakeDevice(int type)
	{
		if (!_selectedRoom) return;

		_selectedRoom.Type = (RoomType)type;

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
}
