using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Build : MonoBehaviour
{

	private static List<Build> _created = new List<Build>();
	 
	public static Build Create(RoomPosition pos)
	{
		return Create(pos.x, pos.y);
	}
	public static Build Create(int x, int y)
	{
		GameObject obj = Instantiate(GameLogic.Instance.BuildPrefab);
		Build build = obj.GetComponent<Build>();

		build.Position.x = x;
		build.Position.y = y;

		_created.Add(build);

		return build;
	}

	public static void Clear()
	{
		foreach (Build build in _created)
		{
			Destroy(build.gameObject);
		}
		_created.Clear();
	}

	public RoomPosition Position;

	// Use this for initialization
	void Start()
	{
		transform.position = RoomPosition.Vector3(Position);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseUp()
	{
		Room room = GameLogic.CreateRoom(Position);
		Clear();
		Gui.StartBuildRoom(room);
	}
}
