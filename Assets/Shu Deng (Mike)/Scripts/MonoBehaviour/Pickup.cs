using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Pickup: MonoBehaviour
{
    private bool InitSound;
    public enum CallbackBehaviour
    {
        OnPicked,
        OnDestroyed
    }
    public CallbackBehaviour effectiveTime = CallbackBehaviour.OnDestroyed;
    [Tooltip("Frequency at which the item will move up and down")]
    public float verticalBobFrequency = 1f;
    [Tooltip("Distance the item will move up and down")]
    public float bobbingAmount = 0.5f;
    [Tooltip("Rotation angle per second")]
    public float rotatingSpeed = 360f;
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
        CustomUIPosition,
    }
    [Tooltip("Target in screen to fly to")]
    public Target flyingTargetInScreen = Target.TopLeft;
    public enum Anchor
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
    public Anchor uIPositionAnchor = Anchor.BottomLeft;
    public Vector2 customUITarget = new Vector2(0, 0);
    public float targetOffsetToScreen = 5f;
    public float flyingTime = 0.5f;
    public AnimationCurve flyingPattern;
    [HideInInspector]
    public static List<GameObject> destroyedPickupList = new List<GameObject>();

    private Collider m_Collider;
    private Vector3 m_StartPosition;
    private Vector3 m_FlyingTarget;    
    enum State
    {
        BEFOREPICKED,
        AFTERPICKED
    };
    State pickupState;

    void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider.isTrigger = true;
        m_StartPosition = transform.position;
        pickupState = State.BEFOREPICKED;
    }

    void Start()  // Dont move the following codes into Awake, for they need Canvas Scaler to awake first
    {
        switch (flyingTargetInScreen)
        {
            case Target.TopLeft:
                m_FlyingTarget = new Vector3(0, GameManager.MainCamera.pixelHeight, targetOffsetToScreen);
                break;
            case Target.TopRight:
                m_FlyingTarget = new Vector3(GameManager.MainCamera.pixelWidth, GameManager.MainCamera.pixelHeight, targetOffsetToScreen);
                break;
            case Target.BottomLeft:
                m_FlyingTarget = new Vector3(0, 0, targetOffsetToScreen);
                break;
            case Target.BottomRight:
                m_FlyingTarget = new Vector3(GameManager.MainCamera.pixelWidth, 0, targetOffsetToScreen);
                break;
            case Target.CustomUIPosition:
                float width = GameManager.HUD.GetComponent<RectTransform>().rect.width;
                float height = GameManager.HUD.GetComponent<RectTransform>().rect.height;
                switch (uIPositionAnchor)
                {
                    case Anchor.BottomLeft:
                        m_FlyingTarget.x = customUITarget.x;// width * GameManager.MainCamera.pixelWidth;
                        m_FlyingTarget.y = customUITarget.y;// height * GameManager.MainCamera.pixelHeight;                        
                        break;
                    case Anchor.BottomRight:
                        m_FlyingTarget.x = (1 + customUITarget.x / width) * GameManager.MainCamera.pixelWidth;
                        m_FlyingTarget.y = customUITarget.y / height * GameManager.MainCamera.pixelHeight;
                        break;
                    case Anchor.TopLeft:
                        m_FlyingTarget.x = customUITarget.x / width * GameManager.MainCamera.pixelWidth;
                        m_FlyingTarget.y = (1 + customUITarget.y / height) * GameManager.MainCamera.pixelHeight;
                        break;
                    case Anchor.TopRight:
                        m_FlyingTarget.x = (1 + customUITarget.x / width) * GameManager.MainCamera.pixelWidth;
                        m_FlyingTarget.y = (1 + customUITarget.y / height) * GameManager.MainCamera.pixelHeight;
                        break;
                }
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
            ResetPickupTransform();
            GameObject clone = Instantiate(gameObject, transform.parent);
            clone.SetActive(false);
            destroyedPickupList.Add(clone);

            StartCoroutine(PlayFlyingEffect());
            m_Collider.enabled = false;
            if (effectiveTime == CallbackBehaviour.OnPicked)
            {
                SendMessage("OnPickedUp");
            }            

            if (InitSound == false)
            {
                InitSound = true;
                PlaySoundOneShot("event:/Characters/Player/Gear Pickup");
            }
        }
    }

    private IEnumerator PlayFlyingEffect()
    {
        Vector3 target = GameManager.MainCamera.ScreenToWorldPoint(m_FlyingTarget);
        float distance = (transform.position - target).magnitude;
        Matrix4x4 matrix = Matrix4x4.LookAt(target, transform.position, Vector3.up);
        float flyingSpeed = distance / flyingTime;

        float z = distance;
        while (z > 0)
        {
            float y = flyingPattern.Evaluate(z / distance);
            Vector3 curTarget = GameManager.MainCamera.ScreenToWorldPoint(m_FlyingTarget);
            transform.position = matrix.MultiplyPoint3x4(new Vector3(0, y, z)) + curTarget - target;
            z -= flyingSpeed * Time.deltaTime;
            yield return null;
        }
        
        if (effectiveTime == CallbackBehaviour.OnDestroyed)
        {
            SendMessage("OnPickedUp");
        }
        Destroy(gameObject);
    }

    void PlaySoundOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GameManager.MainCamera.transform.position);
    }

    private void PlayPickupFeedback()
    {
        if (pickupSFX)
        {
            //AudioUtility.CreateSFX(pickupSFX, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            // To be added with the audio utility
        }

        if (pickupVFXPrefab)
        {
            //var pickupVFXInstance = Instantiate(pickupVFXPrefab, transform.position, Quaternion.identity);
        }
    }

    void ResetPickupTransform()
    {
        transform.position = m_StartPosition;
        transform.localEulerAngles = Vector3.zero;
    }

    public static void RespawnDestroyedPickup()
    {
        foreach (var obj in destroyedPickupList)
        {
            obj.SetActive(true);
        }
        destroyedPickupList.Clear();
    }

    public static void ResetDestroyedPickupList()
    {
        foreach (var obj in destroyedPickupList)
        {
            Destroy(obj);
        }
        destroyedPickupList.Clear();
    }
}
