using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class Waypoint_editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Waypoint waypoint = (Waypoint)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add waypoint"))
        {
            waypoint.BuildObj();
        }

        if(GUILayout.Button("Remove waypoint"))
        {
            waypoint.RemoveObj();
        }

        GUILayout.EndHorizontal();
    }
}
