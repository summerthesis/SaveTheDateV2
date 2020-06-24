﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Pickup: MonoBehaviour
{
    [Header("Effects before picked up")]
    [Tooltip("Frequency at which the item will move up and down")]
    public float verticalBobFrequency = 1f;
    [Tooltip("Distance the item will move up and down")]
    public float bobbingAmount = 0.5f;
    [Tooltip("Rotation angle per second")]
    public float rotatingSpeed = 360f;

    [Header("Effects after picked up")]
    [Tooltip("Sound played on pickup")]
    public AudioClip pickupSFX;
    [Tooltip("VFX spawned on pickup")]
    public GameObject pickupVFXPrefab;   
    public enum Target
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Custom
    }
    [Tooltip("Target in screen to fly to")]
    public Target flyingTargetInScreen = Target.TopLeft;
    [Tooltip("If choose Custom, put UI RectTransform here")]
    public RectTransform customTarget;
    public float targetOffsetToScreen = 5f;
    public float flyingTime = 0.5f;
    public AnimationCurve flyingPattern;

    Collider m_Collider;
    Vector3 m_StartPosition;
    Vector3 m_FlyingTarget;
    enum State
    {
        BEFOREPICKED,
        AFTERPICKED
    };
    State pickupState;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider.isTrigger = true;
        m_StartPosition = transform.position;
        pickupState = State.BEFOREPICKED;
        switch (flyingTargetInScreen)
        {
            case Target.TopLeft:
                m_FlyingTarget = new Vector3(0, Camera.main.pixelHeight, targetOffsetToScreen);
                break;
            case Target.TopRight:
                m_FlyingTarget = new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, targetOffsetToScreen);
                break;
            case Target.BottomLeft:
                m_FlyingTarget = new Vector3(0, 0, targetOffsetToScreen);
                break;
            case Target.BottomRight:
                m_FlyingTarget = new Vector3(Camera.main.pixelWidth, 0, targetOffsetToScreen);
                break;
            case Target.Custom:
                float scale = customTarget.gameObject.GetComponentInParent<CanvasScaler>().scaleFactor;
                m_FlyingTarget = customTarget.position / scale;
                m_FlyingTarget.z = targetOffsetToScreen;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupState == State.BEFOREPICKED)
        {
            float bobbingAnimationPhase = ((Mathf.Sin(Time.time * verticalBobFrequency) * 0.5f) + 0.5f) * bobbingAmount;
            transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;
            transform.Rotate(Vector3.up, rotatingSpeed * Time.deltaTime, Space.Self);
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        KH_PlayerController pickingPlayer = other.GetComponent<KH_PlayerController>();        
        if (pickingPlayer != null)
        {
            pickupState = State.AFTERPICKED;
            StartCoroutine(PlayFlyingEffect());
            m_Collider.enabled = false;
            SendMessage("OnPickedUp");
            PlayPickupFeedback();
        }
    }

    private IEnumerator PlayFlyingEffect()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(m_FlyingTarget);
        float distance = (transform.position - target).magnitude;
        Matrix4x4 matrix = Matrix4x4.LookAt(target, transform.position, Vector3.up);
        float flyingSpeed = distance / flyingTime;

        float z = distance;
        while (z > 0)
        {
            float y = flyingPattern.Evaluate(z / distance);
            transform.position = matrix.MultiplyPoint3x4(new Vector3(0, y, z));
            z -= flyingSpeed * Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }

    public void PlayPickupFeedback()
    {
        if (pickupSFX)
        {
            //AudioUtility.CreateSFX(pickupSFX, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            // To be added with the audio utility
        }

        if (pickupVFXPrefab)
        {
            var pickupVFXInstance = Instantiate(pickupVFXPrefab, transform.position, Quaternion.identity);
        }
    }
}
