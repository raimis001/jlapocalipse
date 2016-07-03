using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
	private float _value;
	public float Value 
	{ 
		get { return _value; }
		set {
			_value = Mathf.Clamp(value,0,1f);
			OnValueChange();
		}
	}

	protected virtual void OnValueChange() 
	{
		Debug.Log("progress var value changed:" + _value.ToString("0.00"));
	}

}
