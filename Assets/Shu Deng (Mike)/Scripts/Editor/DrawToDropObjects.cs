using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using FMOD;

public class DrawToDropObjects : EditorWindow
{
    private GameObject m_ObjectToDrop;
    private Transform m_StartTransform, m_EndTransform, m_SurfaceAnchor, m_GroupTransform;
    private AnimationCurve m_Curve = new AnimationCurve();
    private float m_Spacing;
    private int m_Index;

    private static GUIContent m_SurfaceAnchorText = new GUIContent("Layout Surface Anchor", 
        "Anchor point that locates a layout surface with start and end, will be assigned with +y direction if omitted");
    private static GUIContent m_LayoutPatternText = new GUIContent("Layout Pattern", 
        "Only y-axis (value) matters for the layout, x-axis will be auto-scaled");

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Draw To Drop Objects")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        DrawToDropObjects window = (DrawToDropObjects)EditorWindow.GetWindow(typeof(DrawToDropObjects));
        window.Show();
    }

    private void Update()
    {
               
    }

    public void OnGUI()
    {       
        GUILayout.Label("Game Object Prefab", EditorStyles.boldLabel);
        m_ObjectToDrop = (GameObject)EditorGUILayout.ObjectField("Object To Drop", m_ObjectToDrop, typeof(GameObject), false);

        GUILayout.Label("Game Object Layout", EditorStyles.boldLabel);
        m_StartTransform = (Transform)EditorGUILayout.ObjectField("Start Location", m_StartTransform, typeof(Transform), true);
        m_EndTransform = (Transform)EditorGUILayout.ObjectField("End Location", m_EndTransform, typeof(Transform), true);        
        m_SurfaceAnchor = (Transform)EditorGUILayout.ObjectField(m_SurfaceAnchorText, m_SurfaceAnchor, typeof(Transform), true);
        m_Curve = EditorGUILayout.CurveField(m_LayoutPatternText, m_Curve);
        m_Spacing = EditorGUILayout.FloatField("Object Spacing", m_Spacing);            

        GUILayout.Label("Game Object Properties", EditorStyles.boldLabel);
        m_Index = EditorGUILayout.IntField("Starting Index", m_Index);

        if (GUILayout.Button("Create Objects Group"))
        {
            // Create parent
            Transform parent = new GameObject().transform;
            Undo.RegisterCreatedObjectUndo(parent.gameObject, "Created Objects Group");
            parent.name = "_Group_" + m_ObjectToDrop.name + "_" + m_Index;
            parent.position = (m_StartTransform.position + m_EndTransform.position) * 0.5f;
                        
            // Make copy of startpoint, endpoint and anchor
            Transform tempTransform = new GameObject().transform;
            tempTransform.name = "_Start Point";
            tempTransform.position = m_StartTransform.position;
            m_StartTransform = tempTransform;
            m_StartTransform.SetParent(parent, true);

            tempTransform = new GameObject().transform;
            tempTransform.name = "_End Point";
            tempTransform.position = m_EndTransform.position;
            m_EndTransform = tempTransform;
            m_EndTransform.SetParent(parent, true);

            tempTransform = new GameObject().transform;
            tempTransform.name = "_Surface Anchor";
            if (m_SurfaceAnchor == null)
            {
                tempTransform.position = m_StartTransform.position + Vector3.up;
            }
            else
            {
                tempTransform.position = m_SurfaceAnchor.position;
            }            
            m_SurfaceAnchor = tempTransform;
            m_SurfaceAnchor.SetParent(parent, true);

            // Create target objects
            Matrix4x4 matrix = Matrix4x4.LookAt(m_StartTransform.position, m_EndTransform.position, m_SurfaceAnchor.position - m_StartTransform.position);
            float distance = (m_EndTransform.position - m_StartTransform.position).magnitude;
            int numbers = (int)(distance / m_Spacing) + 1;
            if (m_Curve.length == 0)
            {
                m_Curve.AddKey(0f, 0f);
                m_Curve.AddKey(1f, 0f);
            }
            float ratio = m_Curve.keys[m_Curve.length - 1].time / distance;
            for (int i = 0; i < numbers; ++i)
            {
                GameObject tempGameObject = (GameObject)PrefabUtility.InstantiatePrefab(m_ObjectToDrop);
                tempGameObject.SetActive(true);
                tempGameObject.name = tempGameObject.name + "_" + m_Index;
                ++m_Index;
                tempGameObject.transform.position = matrix.MultiplyPoint3x4(new Vector3(0, m_Curve.Evaluate(m_Spacing * i * ratio), m_Spacing * i));
                tempGameObject.transform.SetParent(parent, true);
            }
            m_Index -= numbers;
            m_GroupTransform = parent;
        }

        GUILayout.Label("Objects Group Update", EditorStyles.boldLabel);
        m_GroupTransform = (Transform)EditorGUILayout.ObjectField("Objects Group", m_GroupTransform, typeof(Transform), true);

        EditorGUILayout.HelpBox("Make sure the correct group object is set before updating!", MessageType.Warning, true);

        if (GUILayout.Button("Update Objects Group"))
        {
            m_StartTransform = m_GroupTransform.GetChild(0);
            m_EndTransform = m_GroupTransform.GetChild(1);
            m_SurfaceAnchor = m_GroupTransform.GetChild(2);

            int numOfChildren = m_GroupTransform.childCount;
            for (int i = 3; i < numOfChildren; ++i)
            {
                DestroyImmediate(m_GroupTransform.GetChild(3).gameObject);
            }            

            Matrix4x4 matrix = Matrix4x4.LookAt(m_StartTransform.position, m_EndTransform.position, m_SurfaceAnchor.position - m_StartTransform.position);
            float distance = (m_EndTransform.position - m_StartTransform.position).magnitude;
            int numbers = (int)(distance / m_Spacing) + 1;
            if (m_Curve.length == 0)
            {
                m_Curve.AddKey(0f, 0f);
                m_Curve.AddKey(1f, 0f);
            }
            float ratio = m_Curve.keys[m_Curve.length - 1].time / distance;
            for (int i = 0; i < numbers; ++i)
            {
                GameObject tempGameObject = (GameObject)PrefabUtility.InstantiatePrefab(m_ObjectToDrop);
                tempGameObject.SetActive(true);
                tempGameObject.name = tempGameObject.name + "_" + m_Index;
                ++m_Index;
                tempGameObject.transform.position = matrix.MultiplyPoint3x4(new Vector3(0, m_Curve.Evaluate(m_Spacing * i * ratio), m_Spacing * i));
                tempGameObject.transform.SetParent(m_GroupTransform, true);
            }
            m_Index -= numbers;
        }
    }
}
