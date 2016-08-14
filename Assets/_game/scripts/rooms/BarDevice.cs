using UnityEngine;
using System;

[Serializable]
public struct BarValues 
{
	public float Damage;
	public float Energy;
	public float Oxigen;
	public float Temperture;
}

public class BarDevice : MonoBehaviour
{

	[Header("Indicators")]
	public ProgressBar DamageProgress;
	public ProgressBar EnergyStorage;
	public ProgressBar OxigenStorage;
	public ProgressBar TemperatureStorage;

	public void Update()
	{
		BarValues values = GameLogic.GetValues();

		if (DamageProgress) DamageProgress.Value = values.Damage;
		if (EnergyStorage) EnergyStorage.Value = values.Energy;
		if (OxigenStorage) OxigenStorage.Value = values.Oxigen;
		if (TemperatureStorage) TemperatureStorage.Value = values.Oxigen;

		/*
	RoomProperty p = Position.Property;
	if (p == null) return; 


	LightsOn = p.Use.Energy <= 1 ? Cave.Storage.Energy > 0.9f : Cave.Storage.Energy >= 0.5f;
	*/
	}
}
