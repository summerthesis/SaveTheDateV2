using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrawToDropObjects : EditorWindow
{
    private GameObject m_ObjectToDrop;
    private Transform m_StartPoint, m_EndPoint;
    private float m_Spacing;
    private int m_Index;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/DrawToDropObjects")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        DrawToDropObjects window = (DrawToDropObjects)EditorWindow.GetWindow(typeof(DrawToDropObjects));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Game Object Prefab", EditorStyles.boldLabel);
        m_ObjectToDrop = (GameObject)EditorGUILayout.ObjectField("Object To Drop", m_ObjectToDrop, typeof(GameObject), false);

        GUILayout.Label("Game Object Layout", EditorStyles.boldLabel);
        m_StartPoint = (Transform)EditorGUILayout.ObjectField("Start Location", m_StartPoint, typeof(Transform), true);
        m_EndPoint = (Transform)EditorGUILayout.ObjectField("End Location", m_EndPoint, typeof(Transform), true);
        m_Spacing = EditorGUILayout.FloatField("Object Spacing", m_Spacing);

        GUILayout.Label("Game Object Properties", EditorStyles.boldLabel);
        m_Index = EditorGUILayout.IntField("Starting Index", m_Index);

        if (GUILayout.Button("Create Objects"))
        {
            Vector3 displacement = (m_EndPoint.position - m_StartPoint.position).normalized * m_Spacing;
            int numbers = (int)((m_EndPoint.position - m_StartPoint.position).magnitude / m_Spacing);
            for (int i = 0; i < numbers; ++i)
            {
                GameObject temp = (GameObject)PrefabUtility.InstantiatePrefab(m_ObjectToDrop);
                temp.SetActive(true);
                temp.name = temp.name + "_" + m_Index;
                ++m_Index;
                temp.transform.position = m_StartPoint.position + displacement * i;
            }            
        }        
    }
}
