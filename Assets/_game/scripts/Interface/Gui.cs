using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour
{
#region static
	private static Gui _instance;
	public static Gui Instance
	{
		get { return _instance; }
	}

	private static GameObject _selectedObject;
	public static GameObject SelectedObject
	{
		get { return _selectedObject; }
		set
		{
			if (_selectedObject == value)
			{
				Debug.Log("Select same object");
				return;
			}

			CanvasObject obj;
			if (_selectedObject)
			{
				obj = _selectedObject.GetComponent<CanvasObject>();
				if (obj) obj.Deselect();
				_selectedObject = null;
				CloseMenu();
			}

			_selectedObject = value;
			if (_selectedObject)
			{
				obj = _selectedObject.GetComponent<CanvasObject>();
				if (!obj)
				{
					_selectedObject = null;
					CloseMenu();
					return;
				}
				obj.Select();
			}
		}
	}
#endregion

	[Header("Object menu")] 
	public GameObject MainMenu;
	public GameObject MenuDrone;

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		if (MainMenu) MainMenu.SetActive(false);
	}

	void ClickButton(string tag)
	{
		Debug.Log("Button: " + tag);
		if (SelectedObject)
		{
			SelectedObject.GetComponent<CanvasObject>().ButtonClick(tag);
		}
	}

	public static void CloseAllMenu()
	{
		if (!_instance) return;
		if (_instance.MenuDrone) _instance.MenuDrone.SetActive(false);

	}

	public static void CloseMenu()
	{
		if (!_instance) return;
		if (_instance.MainMenu) _instance.MainMenu.SetActive(false);
	}

	public static void OpenMenu(int menu)
	{
		if (!_instance) return;
		if (_instance.MainMenu) _instance.MainMenu.SetActive(true);
	
		//TODO: cloase all menu
		CloseAllMenu();

		switch (menu)
		{
			case 1:
				if (_instance.MenuDrone) _instance.MenuDrone.SetActive(true);
				break;
		}
	}

}
