using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DrawToDropObjects : EditorWindow
{
    private GameObject m_ObjectToDrop;
    private Transform m_StartTransform, m_EndTransform, m_GroupTransform;
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

    private void Update()
    {
        if (m_StartTransform != null && m_EndTransform != null)
        {
            Debug.DrawLine(m_StartTransform.position, m_EndTransform.position, Color.white);
        }        
    }

    public void OnGUI()
    {       
        GUILayout.Label("Game Object Prefab", EditorStyles.boldLabel);
        m_ObjectToDrop = (GameObject)EditorGUILayout.ObjectField("Object To Drop", m_ObjectToDrop, typeof(GameObject), false);

        GUILayout.Label("Game Object Layout", EditorStyles.boldLabel);
        m_StartTransform = (Transform)EditorGUILayout.ObjectField("Start Location", m_StartTransform, typeof(Transform), true);
        m_EndTransform = (Transform)EditorGUILayout.ObjectField("End Location", m_EndTransform, typeof(Transform), true);
        m_Spacing = EditorGUILayout.FloatField("Object Spacing", m_Spacing);            

        GUILayout.Label("Game Object Properties", EditorStyles.boldLabel);
        m_Index = EditorGUILayout.IntField("Starting Index", m_Index);

        if (GUILayout.Button("Create Objects Group"))
        {
            // Create parent
            GameObject parent = new GameObject();            
            parent.name = "Group_" + m_ObjectToDrop.name + "_" + m_Index;
            parent.transform.position = (m_StartTransform.position + m_EndTransform.position) * 0.5f;

            m_StartTransform = Instantiate(m_StartTransform.gameObject, m_StartTransform.position, m_StartTransform.rotation).transform;
            m_StartTransform.SetParent(parent.transform, true);
            m_EndTransform = Instantiate(m_EndTransform.gameObject, m_EndTransform.position, m_EndTransform.rotation).transform;
            m_EndTransform.SetParent(parent.transform, true);

            // Create target objects
            Vector3 displacement = (m_EndTransform.position - m_StartTransform.position).normalized * m_Spacing;
            int numbers = (int)((m_EndTransform.position - m_StartTransform.position).magnitude / m_Spacing);
            for (int i = 0; i < numbers; ++i)
            {
                GameObject temp = (GameObject)PrefabUtility.InstantiatePrefab(m_ObjectToDrop);
                temp.SetActive(true);
                temp.name = temp.name + "_" + m_Index;
                ++m_Index;
                temp.transform.position = m_StartTransform.position + displacement * i;
                temp.transform.SetParent(parent.transform, true);
            }
            m_Index -= numbers;
            m_GroupTransform = parent.transform;
        }

        GUILayout.Label("Objects Group Update", EditorStyles.boldLabel);
        m_GroupTransform = (Transform)EditorGUILayout.ObjectField("Objects Group", m_GroupTransform, typeof(Transform), true);

        EditorGUILayout.HelpBox("Make sure the correct group object is set before updating!", MessageType.Warning, true);

        if (GUILayout.Button("Update Objects Group"))
        {
            m_StartTransform = m_GroupTransform.GetChild(0);
            m_EndTransform = m_GroupTransform.GetChild(1);

            int numOfChildren = m_GroupTransform.childCount;
            for (int i = 2; i < numOfChildren; ++i)
            {
                DestroyImmediate(m_GroupTransform.GetChild(2).gameObject);
            }            

            Vector3 displacement = (m_EndTransform.position - m_StartTransform.position).normalized * m_Spacing;
            int numbers = (int)((m_EndTransform.position - m_StartTransform.position).magnitude / m_Spacing);
            for (int i = 0; i < numbers; ++i)
            {
                GameObject temp = (GameObject)PrefabUtility.InstantiatePrefab(m_ObjectToDrop);
                temp.SetActive(true);
                temp.name = temp.name + "_" + m_Index;
                ++m_Index;
                temp.transform.position = m_StartTransform.position + displacement * i;
                temp.transform.SetParent(m_GroupTransform, true);
            }
            m_Index -= numbers;
        }
    }
}
