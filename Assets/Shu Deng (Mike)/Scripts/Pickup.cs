using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        BottomRight
    }
    [Tooltip("Target in screen to fly to")]
    public Target flyingTargetInScreen;
    public float flyingSpeed;
    [Tooltip("Speed of scaling down")]
    public float scalingDownSpeed;

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
                m_FlyingTarget = new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane);
                break;
            case Target.TopRight:
                m_FlyingTarget = new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane);
                break;
            case Target.BottomLeft:
                m_FlyingTarget = new Vector3(0, 0, Camera.main.nearClipPlane);
                break;
            case Target.BottomRight:
                m_FlyingTarget = new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane);
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
        else if (pickupState == State.AFTERPICKED)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(m_FlyingTarget);
            Vector3 translation = flyingSpeed * (target - transform.position).normalized;            
            transform.Translate(translation, Space.World);
            transform.localScale = transform.localScale - new Vector3(scalingDownSpeed, scalingDownSpeed, scalingDownSpeed);
            if (transform.localScale.x < 0 || Vector3.Distance(transform.position, target) < flyingSpeed)
            {
                Destroy(gameObject);
            }
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        KH_PlayerController pickingPlayer = other.GetComponent<KH_PlayerController>();        
        if (pickingPlayer != null)
        {
            pickupState = State.AFTERPICKED;
            m_Collider.enabled = false;
            PlayPickupFeedback();
        }
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
