using UnityEngine;
using System.Collections;

public enum GrowStatus
{
	None,
	Growing,
	Ripe
}

[ExecuteInEditMode]
public class Vaga : MonoBehaviour
{

	[Range(0, 1f)]
	public float GrowStage;

	public float GrowTime;
	public float GrowMax = 30;
	public GrowStatus GrowStatus;

	public Transform Laksti;

	public int Value;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (Application.isPlaying)
		{
			if (GrowStatus != GrowStatus.Growing) return;

			GrowTime += Time.deltaTime;
			GrowStage = GrowTime / GrowMax;
			if (GrowStage >= 1)
			{
				GrowTime = 0;
			}
		}

		if (Laksti) Laksti.localScale = new Vector3(0.5f + GrowStage * 0.5f, GrowStage, 1);

	}

	public void OnMouseUp()
	{
		Debug.Log("Vaga click:" + GrowStatus);
	}
}
