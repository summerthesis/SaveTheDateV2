using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pickup)), CanEditMultipleObjects]
public class PickupEditor : Editor
{
    private SerializedProperty effectiveTimeProp, vericalBobFreProp, bobbingAmountProp,
        rotatingSpeedProp, pickupSFXProp, pickupVFXPrefabProp, flyingTarProp, uIPosAnchorProp,
        customUITarProp, targetOffsetProp, flyingTimeProp, flyingPatternProp;

    private void OnEnable()
    {
        effectiveTimeProp = serializedObject.FindProperty("effectiveTime");
        vericalBobFreProp = serializedObject.FindProperty("verticalBobFrequency");
        bobbingAmountProp = serializedObject.FindProperty("bobbingAmount");
        rotatingSpeedProp = serializedObject.FindProperty("rotatingSpeed");
        pickupSFXProp = serializedObject.FindProperty("pickupSFX");
        pickupVFXPrefabProp = serializedObject.FindProperty("pickupVFXPrefab");
        flyingTarProp = serializedObject.FindProperty("flyingTargetInScreen");
        uIPosAnchorProp = serializedObject.FindProperty("uIPositionAnchor");
        customUITarProp = serializedObject.FindProperty("customUITarget");
        targetOffsetProp = serializedObject.FindProperty("targetOffsetToScreen");
        flyingTimeProp = serializedObject.FindProperty("flyingTime");
        flyingPatternProp = serializedObject.FindProperty("flyingPattern");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUILayout.Label("Behaviour", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(effectiveTimeProp);
        GUILayout.Label("Effects before picked up", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(vericalBobFreProp);
        EditorGUILayout.PropertyField(bobbingAmountProp);
        EditorGUILayout.PropertyField(rotatingSpeedProp);
        GUILayout.Label("Effects after picked up", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(pickupSFXProp);
        EditorGUILayout.PropertyField(pickupVFXPrefabProp);
        EditorGUILayout.PropertyField(flyingTarProp);
        if (flyingTarProp.enumValueIndex == 4)  // CustomUIPosition
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField(uIPosAnchorProp);
            EditorGUILayout.PropertyField(customUITarProp);
            --EditorGUI.indentLevel;
        }
        EditorGUILayout.PropertyField(targetOffsetProp);
        EditorGUILayout.PropertyField(flyingTimeProp);
        EditorGUILayout.PropertyField(flyingPatternProp);
        serializedObject.ApplyModifiedProperties();
    }
}
