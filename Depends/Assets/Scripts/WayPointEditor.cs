using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPoint))] //WayPoint 대해서 custom

public class WayPointEditor : Editor
{
    WayPoint wayPoint => target as WayPoint;

    private void OnSceneGUI()
    {
        for(int i = 0; i < wayPoint.Points.Length; i++) //WayPoint에 Points의 길이만큼 반복한다.
        {
            EditorGUI.BeginChangeCheck();

            //create handle
            Vector3 currentWaypointPoint = wayPoint.CurrentPosition + wayPoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, 
                                       Quaternion.identity, 0.7f, new Vector3(x:0.3f, y:0.3f, z:0.3f), 
                                       Handles.SphereHandleCap);

            //nubering handle
            GUIStyle numStyle = new GUIStyle();
            numStyle.fontSize = 10;
            numStyle.fontStyle = FontStyle.Bold;
            numStyle.normal.textColor = Color.white;
            Vector3 textAllingnment = Vector3.down * 0.35f + Vector3.right * 0.35f;

            Handles.Label(wayPoint.CurrentPosition + wayPoint.Points[i] + textAllingnment, 
                          text: $"{i + 1}", numStyle);

            EditorGUI.EndChangeCheck();

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, name: "Free Move Handle");
                wayPoint.Points[i] = newWaypointPoint-wayPoint.CurrentPosition;
            }
        }
    }
}