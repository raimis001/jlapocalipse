using UnityEngine;
using System.Collections;

public abstract class CanvasObject : MonoBehaviour
{

	public abstract void Deselect();
	public abstract void Select();
	public abstract void ButtonClick(string tag);
	public abstract void BuildRoom(Room room);

	void OnMouseUp()
	{
		Gui.SelectedObject = gameObject;
	}

}
