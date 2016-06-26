using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Digits7 : MonoBehaviour
{

	[Range(0,9)]
	public int Number;

	public GameObject[] Digits;

	int _number = -1;

	static Dictionary<int, int[]> numbers = new Dictionary<int, int[]>()
	{
		{0, new int[7] {1,1,1,1,1,1,0 } },
		{1, new int[7] {1,1,0,0,0,0,0 } },
		{2, new int[7] {1,0,1,1,0,1,1 } },
		{3, new int[7] {1,1,1,0,0,1,1 } },
		{4, new int[7] {1,1,0,0,1,0,1 } },
		{5, new int[7] {0,1,1,0,1,1,1 } },
		{6, new int[7] {0,1,1,1,1,1,1 } },
		{7, new int[7] {1,1,0,0,0,1,0 } },
		{8, new int[7] {1,1,1,1,1,1,1 } },
		{9, new int[7] {1,1,1,0,1,1,1 } },
	};

	void Start()
	{

	}

	void Update()
	{
		if (_number != Number) {
			_number = Number;
			DrawDigit();
		}
	}

	void DrawDigit() 
	{
		//Debug.Log("Draw digit:" + Number);
		int[] r = numbers[Mathf.Abs(Number)];
		for (int i = 0; i < Digits.Length; i++) {
			Digits[i].SetActive(r[i] == 1);
		}
	}
}
