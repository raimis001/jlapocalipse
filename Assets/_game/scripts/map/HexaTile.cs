using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HexaTile : MonoBehaviour
{
	public GameObject Buildings;
	public GameObject Clouds;

	public GameObject Selection;
	public LineDrawHexa LineToTarget;

	public TileClass Setup;

	public TilePos Position;
	private TilePos _position;

	public void Start()
	{
		if (Clouds)
		{
			//Clouds.SetActive(false);
		}
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

	public void OnMouseDown()
	{
		HexaMap.Instance.Line.Parent = this;
		HexaMap.Instance.Line.draw = true;

		HexaMap.Instance.SelectedTile = this;
	}

	public void OnMouseOver()
	{
		//Debug.Log("Mouse over:" + _position);
		if (HexaMap.Instance.Line.draw)
		{
			if (!HexaMap.LastTile)
			{
				HexaMap.LastTile = this;
				//Selection.SetActive(true);
				return;
			}

			if (HexaMap.LastTile.Position.HashString == Position.HashString)
			{
				return;
			}

			HexaMap.LastTile.LineToTarget.Target = this;
			HexaMap.LastTile = this;
			//Selection.SetActive(true);
		}
	}

	public void OnMouseDrag()
	{
		//Debug.Log("Mouse dragg");

	}

	public void OnMouseUp()
	{
		Debug.Log(_position.HashString);
		HexaMap.Instance.Line.draw = false;
	}

}
