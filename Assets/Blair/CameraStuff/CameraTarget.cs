using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class CameraTarget : MonoBehaviour
{
    private PlayerInputAction controls;
    public GameObject mPlayer;
    public Quaternion[] CameraAngles;
    private Quaternion CurrentAngle;
    public Quaternion TargetAngle;
    public int AngleState;
    bool AngleSet;
    
    void Awake()
    {
        AngleState = 7; CurrentAngle = CameraAngles[AngleState];
            
        controls = new PlayerInputAction();
        controls.CameraDebugAngles.CycleAngles.performed += ctx => CycleAngle();
        
    }
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");   
    }

    void Update()
    {
        CurrentAngle = this.transform.rotation;
        transform.position = mPlayer.transform.position;

        if(!AngleSet)
        {
            if (CurrentAngle == TargetAngle)
            {
            AngleSet = true; 
            }
            else
            {
            Tween mTween = this.transform.DORotateQuaternion(CameraAngles[AngleState], 1);
            }

        }

    }
    
    void CycleAngle()
    {
        if(AngleState == 7)
        {
            AngleState = 0;
            AngleSet = false;
        }
        else
        {
            AngleState++;
            AngleSet = false;
        }
        TargetAngle = CameraAngles[AngleState];
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
