using UnityEngine;
using System.Collections;

public class HexaTile : MonoBehaviour
{
	public GameObject Buildings;
	public GameObject Clouds;

	public TileClass Setup;

	public void Start()
	{
		Clouds.SetActive(true);
		Buildings.SetActive(true);
	}

	public void OnMouseUp()
	{
		Debug.Log(Setup);
	}
}
