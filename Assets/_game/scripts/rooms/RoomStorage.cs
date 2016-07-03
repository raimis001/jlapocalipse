﻿using UnityEngine;
using System.Collections;

public class RoomStorage : RoomDevice
{
	private float _temperature = 12;
	private int _display 
	{
		get { return (int)_temperature; }
	}
	public BarSlider Slider;

	public Digits7[] Display;
	public GameObject Sign;

	public Inventory[] Inventorys;

	protected override void Start()
	{
		base.Start();
		Inventorys[0].AddItem(ItemMain.Create(ItemKind.Ammo));
	}

	protected override void Update()
	{
		base.Update();

		if (!Slider) return;

		int t = LightsOn ? (int)((60f * Slider.Value) - 30f) : 12;

		if (_display == t) return;

		float lerp = 10 * (Mathf.Abs(_display - t) / 60f);

		_temperature +=  lerp * Time.deltaTime * (_temperature < t ? 1 : -1) ;
		DrawDisplay();

	}

	void DrawDisplay() 
	{
		Sign.SetActive(Mathf.Sign(_display) < 0);

		int d = _display / 10;
		int n = _display - d * 10;

		Display[0].Number =  d;
		Display[1].Number = n;
	}

	public void PowerSwitch() 
	{
		LightsOn = !LightsOn;
	}
}
