using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour
{

	public static Build CreateBuild(RoomPosition pos)
	{
		return CreateBuild(pos.x, pos.y);
	}
	public static Build CreateBuild(int x, int y)
	{
		GameObject obj = Instantiate(GameLogic.Instance.BuildPrefab);
		Build build = obj.GetComponent<Build>();

		build.Position.x = x;
		build.Position.y = y;

		return build;
	}

	public RoomPosition Position = new RoomPosition();

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
		GameLogic.CreateRoom(Position);
	}
}
