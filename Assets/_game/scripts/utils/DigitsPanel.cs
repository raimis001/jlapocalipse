using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DigitsPanel : MonoBehaviour
{

	public GameObject Sign;
	public Digits7[] Digits;

	private int _number = int.MaxValue;
	public int Number;

	// Use this for initialization
	void Start()
	{
		_number = Number;
		DrawDisplay();
	}

	// Update is called once per frame
	void Update()
	{
		if (_number != Number)
		{
			_number = Number;
			DrawDisplay();
		}
	}

	void DrawDisplay()
	{
		if (Sign) Sign.SetActive(Mathf.Sign(_number) < 0);

		string num = Mathf.Abs(_number).ToString();

		if (num.Length == 0) return;

		if (num.Length == 1)
		{
			if (Digits[0]) Digits[0].Number = -1;
			if (Digits[1]) Digits[1].Number = int.Parse(num);
			return;
		}

		for (int i = 0; i < num.Length; i++)
		{
			if (i >= Digits.Length) break;
			if (Digits[i]) Digits[i].Number = int.Parse(num[i].ToString()); ;
		}

	}
}
