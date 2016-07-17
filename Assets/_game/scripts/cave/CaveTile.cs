using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CaveTile : MonoBehaviour
{

	public RoomPosition Position;



	// Update is called once per frame
	void Update()
	{
		if (!Application.isPlaying)
		{
			transform.localPosition = new Vector3(Position.x*10f, Position.y*-8f, 0f);
		}
	}
}
