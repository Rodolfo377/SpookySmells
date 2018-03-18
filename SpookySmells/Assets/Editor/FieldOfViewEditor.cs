using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FieldOfView))]
public class FieldOfViewEditor : Editor {

    private void OnSceneGUI()
    {
       
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.back, Vector3.right, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirectionFromAngle((-fow.viewAngle / 2)+90, false);
        Vector3 viewAngleB = fow.DirectionFromAngle((fow.viewAngle / 2)+90, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position +  viewAngleA* fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position +  viewAngleB* fow.viewRadius);
        Handles.color = Color.red;
        
        Debug.Log("Does this thing even work");
        foreach(Transform shit in fow.visibleTargets)
        {
            Debug.Log("Drawing...");
            Handles.DrawLine(fow.transform.position, shit.position);
        }
    }
}
