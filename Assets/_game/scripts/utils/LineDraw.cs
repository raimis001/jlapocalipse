using UnityEngine;
using System.Collections;

public class LineDraw : MonoBehaviour
{

	public Transform Line;
	
	internal bool draw;
	internal Vector3 Target;
	
	public HexaTile Parent
	{
		set
		{
			transform.position = value.transform.position;
			transform.gameObject.SetActive(true);
		}
	}

	void Update()
	{
		if (!draw)
		{
			transform.gameObject.SetActive(false);
			return;
		}

		float distance = Vector3.Distance(transform.position, Target);
		if (distance < 0.5f)
		{
			Line.gameObject.SetActive(false);
			return;
		}

		Line.gameObject.SetActive(true);

		float angle = Helper.AngleInDeg(transform.position,Target);

		Line.localEulerAngles = new Vector3(0,angle,0);
		Line.localScale = new Vector3(1, 1, distance);
	}


}
