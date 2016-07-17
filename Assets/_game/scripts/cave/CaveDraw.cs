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
	public void Update()
	{
		if (Bottom) Bottom.position = new Vector3(0,Y * -8);
		if (Left) Left.position = new Vector3(XLeft * -10, 0);
		if (Right) Right.position = new Vector3(XRight * 10, 0);
	}
}
