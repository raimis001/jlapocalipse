using UnityEngine;
using System.Collections;

public class AnimationEvent : MonoBehaviour
{

	public delegate void AnimationDelegate(string tag);
	public static event AnimationDelegate OnAnimEvent;

	public void AnimEvent(string tag)
	{
		if (OnAnimEvent != null)
		{
			OnAnimEvent(tag);
		}
	}

}
