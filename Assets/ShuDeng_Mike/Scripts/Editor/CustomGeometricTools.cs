using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CustomGeometricTools : EditorWindow
{
    private Transform m_ObjectToRotate;
    private Vector3 m_RotationAxis;
    private Transform m_RotationPoint;
    private float m_RotationAngle;

    [MenuItem("Window/CustomGeometricTools")]
    static void Init()
    {
        CustomGeometricTools window = (CustomGeometricTools)EditorWindow.GetWindow(typeof(CustomGeometricTools));
        window.Show();
    }

    public void OnGUI()
    {
        GUILayout.Label("Rotation Tool", EditorStyles.boldLabel);
        m_ObjectToRotate = (Transform)EditorGUILayout.ObjectField("Target Object", m_ObjectToRotate, typeof(Transform), true);
        m_RotationAxis = EditorGUILayout.Vector3Field("Rotation Axis", m_RotationAxis);
        m_RotationPoint = (Transform)EditorGUILayout.ObjectField("Rotation Point", m_RotationPoint, typeof(Transform), true);
        m_RotationAngle = EditorGUILayout.FloatField("Rotation Angle", m_RotationAngle);
        if (GUILayout.Button("Rotate"))
        {
            Undo.RecordObject(m_ObjectToRotate, "Rotated Transform");
            m_ObjectToRotate.RotateAround(m_RotationPoint.position, m_RotationAxis, m_RotationAngle);
            
        }
        
    }
}
