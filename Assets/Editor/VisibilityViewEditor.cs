using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (Visibility))]
public class VisibilityViewEditor : Editor
{
    
    void OnSceneGUI() {
        Visibility vis = (Visibility)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(vis.transform.position, Vector3.up, Vector3.forward, 360, Visibility.viewDistance);

        float angleA =  vis.transform.eulerAngles.y + Visibility.viewAngle/2;
        float angleB = vis.transform.eulerAngles.y - Visibility.viewAngle/2;
        Vector3 viewAngleA = new Vector3(Mathf.Sin(angleA * Mathf.Deg2Rad),0,Mathf.Cos(angleA * Mathf.Deg2Rad));
        Vector3 viewAngleB = new Vector3(Mathf.Sin(angleB * Mathf.Deg2Rad),0,Mathf.Cos(angleB * Mathf.Deg2Rad));

        Handles.DrawLine(vis.transform.position, vis.transform.position + viewAngleA * Visibility.viewDistance);
        Handles.DrawLine(vis.transform.position, vis.transform.position + viewAngleB * Visibility.viewDistance);
    }

}
