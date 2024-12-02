using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEdit : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Radius);

        Vector3 viewAngleLeft = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.Angle / 2);
        Vector3 viewAngleRight = DirectionFromAngle(fov.transform.eulerAngles.y, fov.Angle / 2);

        Handles.color = Color.yellow;

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.Radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.Radius);

        if (fov.CanSeePlayer)
        {
            Handles.color += Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }
}
