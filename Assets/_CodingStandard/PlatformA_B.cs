using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformA_B : MonoBehaviour
{
    //This Objects starting position is Point A, The GameObject B is Point B.
    public GameObject B; //Set in prefab.
    private Vector3 PointA, PointB;
    public float mSpeed; // The actual speed this moves at.
    public float NormalSpeed; //Set in the inspector.
    private float SlowedSpeed;
    public int IdleDuration;//How long will the object pause at each end.
    private int IdleCount;
    private enum ObjectStates
    {
        MoveA_B, 
        Idling,
        MoveB_A,
        CustomEvent
    };
    ObjectStates ObjectState;
    private bool isActive;
  
    void Start()
    {
        SlowedSpeed = NormalSpeed / 2;
        PointA = this.transform.position;
        PointB = B.transform.position;
        Destroy(B);
        ObjectState = ObjectStates.MoveA_B;
        isActive = true;
        mSpeed = NormalSpeed;
        
    }

    void Update()
    {
        
        if(isActive)
          switch(ObjectState)
            {
                case ObjectStates.MoveA_B:
                    Move(PointB);
                    break;
                
                case ObjectStates.MoveB_A:
                    Move(PointA);
                    break;
                
                case ObjectStates.Idling:
                    IdleCount--;
                    if(IdleCount <= 0) ChangeDirection();
                    break;

                case ObjectStates.CustomEvent:

                    break;

                default:
                    break;
            }

    }

    void Move(Vector3 Point)
    {
        float step = mSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Point, step);
        if (this.transform.position == Point) ObjectState = ObjectStates.Idling;
        IdleCount = IdleDuration;
    }

    void ChangeDirection()
    {
        if (this.transform.position == PointA) ObjectState = ObjectStates.MoveA_B;
        else
        if (this.transform.position == PointB) ObjectState = ObjectStates.MoveB_A;
    }

    void TimeSlow()
    {
        if (mSpeed > SlowedSpeed) mSpeed -= 0.1f;
    }
    void TimeStop()
    {
        mSpeed = 0;
    }
    void TimeFastForward()
    {
        mSpeed = NormalSpeed * 2;
    }
    void RestoreToNormal()
    {
        mSpeed = NormalSpeed;
        IdleCount = IdleDuration;
    }

   
}
