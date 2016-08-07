using UnityEngine;
using System.Collections;

public class LineDrawHexa : MonoBehaviour
{

	public Transform Line;
	public HexaTile Target;

	public void Update()
	{
		if (!Line || !Target) return;

		float angle = Helper.AngleInDeg(transform.position, Target.transform.position);
		float distance = Vector3.Distance(transform.position, Target.transform.position);

		Line.localEulerAngles = new Vector3(0,0, angle);
		Line.localScale = new Vector3(1, distance,1);

	}
}
