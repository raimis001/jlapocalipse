using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointPath))]
public class WaypointPathEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		WaypointPath myScript = (WaypointPath)target;

		EditorGUILayout.LabelField("Waypoints count", myScript.WaypointsCount.ToString());

		if (GUILayout.Button("Add waypoint"))
		{
			myScript.CreateWaypoint();
		}
	}

}

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Waypoint myScript = (Waypoint)target;
		EditorGUILayout.LabelField("Index", myScript.Index.ToString());

		if (GUILayout.Button("Add waypoint"))
		{
			myScript.CreateWaypoint();
		}
	}

}