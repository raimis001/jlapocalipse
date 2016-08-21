using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CaveDraw : MonoBehaviour
{
	public Transform Bottom;
	public Transform Left;
	public Transform Right;

	public int XLeft;
	public int XRight;
	public int Y;

	public static int LastRoomCount;

	public void Update()
	{
		SetBounds();
	}

	void SetBounds() 
	{
		Room[] rooms = FindObjectsOfType<Room>();
		if (rooms.Length == LastRoomCount) return;
		LastRoomCount = rooms.Length;


		int xl = 0;
		int xr = 0;
		int yb = 0;

		foreach (Room room in rooms) 
		{
			if (room.Position.y > 0 && room.Position.x < xl) xl = room.Position.x;
			if (room.Position.y > 0 && room.Position.x > xr) xr = room.Position.x;
			if (room.Position.y > yb) yb = room.Position.y;
		}

		XLeft = xl;
		XRight = xr + 1;
		Y = yb;

		if (Bottom) Bottom.position = new Vector3(0, Y * -8);
		if (Left) Left.position = new Vector3(XLeft * 10, 0);
		if (Right) Right.position = new Vector3(XRight * 10, 0);

	}

}
