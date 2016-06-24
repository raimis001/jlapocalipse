using UnityEngine;
using System.Collections;

public class HexaTile : MonoBehaviour
{
	public GameObject Buildings;
	public GameObject Clouds;

	public TileClass Setup;

	public void Start()
	{
		Clouds.SetActive(false);
		Buildings.SetActive(true);
	}

	public void OnMouseUp()
	{
		Debug.Log(Setup);
	}
}
