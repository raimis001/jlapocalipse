using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HexaTile : MonoBehaviour
{
	public GameObject Buildings;
	public GameObject Clouds;

	public TileClass Setup;

	public TilePos Position;

	private TilePos _position;

	public void Start()
	{
		//Clouds.SetActive(true);
		//Buildings.SetActive(true);
		_position = Position;
	}

	public void Update()
	{
		if (!Application.isPlaying && _position.HashString != Position.HashString)
		{
			//TODO: reposition tile
			_position = Position;
			SetPosition();
		}
	}

	void SetPosition()
	{
		transform.localPosition = _position.Position;
	}

	public void OnMouseUp()
	{
		Debug.Log(Setup);
	}
}
