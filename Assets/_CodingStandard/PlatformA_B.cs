using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformA_B : MonoBehaviour
{
    
    public GameObject B; 
    private Vector3 PointA, PointB; 
    public float StopSpeed, NormalSpeed; 
    private float mSpeed, SlowedSpeed, FastSpeed;
    public int IdleDuration;
    private int IdleCount;
    
    private enum ObjectStates
    {
        Unavailable,
        MoveA_B, 
        Idling,
        MoveB_A,
        CustomEvent
    };
    ObjectStates ObjectState;
  
    void Start()
    {
        SlowedSpeed = NormalSpeed / 2;
        FastSpeed = NormalSpeed * 2;
        StopSpeed = 0;
        PointA = this.transform.position;
        PointB = B.transform.position;
        Destroy(B);
        ObjectState = ObjectStates.MoveA_B;
    
        mSpeed = NormalSpeed;
        
    }

    void Update()
    {     
          switch(ObjectState)
            {
                case ObjectStates.Unavailable:

                    break;

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
        if (this.transform.position == Point)
        {
            ObjectState = ObjectStates.Idling;  
            IdleCount = IdleDuration;
        }
    }

    void ChangeDirection()
    {
        if (this.transform.position == PointA) ObjectState = ObjectStates.MoveA_B;
        if (this.transform.position == PointB) ObjectState = ObjectStates.MoveB_A;
    }

    void TimeSlow()
    {
        mSpeed = SlowedSpeed;
    }
    void TimeStop()
    {
        mSpeed = 0;
    }
    void TimeFastForward()
    {
        mSpeed = FastSpeed;
    }
    void JumpForward()
    {
        if (ObjectState == ObjectStates.MoveA_B)
        {
            Vector3 NewPosition =
            transform.position + transform.TransformDirection(GetLocalDirection(transform, PointB));
            if (Vector3.Distance(transform.position, NewPosition) > Vector3.Distance(transform.position, PointB))
            {
                transform.position = PointB;
            }
            else
                transform.Translate(GetLocalDirection(transform, PointB)*4);
        }
        
        if (ObjectState == ObjectStates.MoveB_A)
        {
            Vector3 NewPosition =
            transform.position + transform.TransformDirection(GetLocalDirection(transform, PointA));
            if (Vector3.Distance(transform.position, NewPosition) > Vector3.Distance(transform.position, PointA))
            {
                transform.position = PointA;
            }
            else
                transform.Translate(GetLocalDirection(transform, PointA)*4);
        }
    }
    void RestoreToNormal()
    {
        mSpeed = NormalSpeed;
    }
    Vector3 GetLocalDirection(Transform transform, Vector3 destination)
    {
        return transform.InverseTransformDirection((destination - transform.position).normalized);
    }

}
