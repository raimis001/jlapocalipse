using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Room : MonoBehaviour
{

	public GameObject SelectedObject;
	public GameObject[] Walls;

	private int _weight;

	[Range(0,3)]
	public int Weight;

	[HideInInspector]
	public RoomType Type;

	public RoomPosition Position = new RoomPosition();

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
	}

	// Update is called once per frame
	void Update()
	{
		if (Weight != _weight)
		{
			_weight = Weight;
			SetWalls();
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

	}

	void OnMouseUp()
	{
		GameLogic.SelectedRoom = this;
	}

}
