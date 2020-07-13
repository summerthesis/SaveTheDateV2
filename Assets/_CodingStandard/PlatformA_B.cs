using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformA_B : MonoBehaviour
{
    
    public GameObject B; 
    private Vector3 PointA, PointB;
    [HideInInspector]
    public float mSpeed, StopSpeed;
    public float  NormalSpeed, SlowFactor, FastFactor; 
    private float SlowedSpeed, FastSpeed;
    public int IdleDuration;
    private int IdleCount;
    [HideInInspector]
    public GameObject mPlayer;
    
    [HideInInspector]
    public Vector3 mPrevPos, mCurrentPos;
    [HideInInspector]
    public float xx, yy, zz;//the change in
    private enum ObjectStates
    {
        Unavailable,
        MoveA_B, 
        Idling,
        MoveB_A,
        CustomEvent
    };
    ObjectStates ObjectState;

    public bool isVertical, Parenting;
    public bool movingUpward, reachedEnd;
    void Start()
    {
        SlowedSpeed = NormalSpeed / SlowFactor;
        FastSpeed = NormalSpeed * FastFactor;
        StopSpeed = 0;
        PointA = this.transform.position;
        PointB = B.transform.position;
        Destroy(B);
        ObjectState = ObjectStates.MoveA_B;
        mSpeed = NormalSpeed;
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
     

        

        switch (ObjectState)
            {
                case ObjectStates.Unavailable:

                    break;

                case ObjectStates.MoveA_B:
                    Move(PointB);
                    if (reachedEnd) reachedEnd = false;
                    break;
                
                case ObjectStates.MoveB_A:
                    Move(PointA);
                    if (reachedEnd) reachedEnd = false;

                    break;
                
                case ObjectStates.Idling:
                    IdleCount--;
                    if(IdleCount <= 0)
                    ChangeDirection();
                    if(IdleCount == IdleDuration-1)
                    {
                    reachedEnd = true;
                    }
                    if(IdleCount < IdleDuration-5 && reachedEnd)
                    {
                    reachedEnd = false;
                    }


                break;

                case ObjectStates.CustomEvent:

                    break;

                default:
                    break;
            }
        mCurrentPos = transform.position;
        xx = mCurrentPos.x - mPrevPos.x;
        yy = mCurrentPos.y - mPrevPos.y;
        zz = mCurrentPos.z - mPrevPos.z;
        if (mCurrentPos.y > mPrevPos.y) { movingUpward = true; }
        if (mCurrentPos.y < mPrevPos.y) { movingUpward = false; }
        if (Parenting)
        {
            mPlayer.transform.position += new Vector3(xx, yy, zz);
        }

        mPrevPos = mCurrentPos;
      

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
        if (this.transform.position == PointA)
        {
            ObjectState = ObjectStates.MoveA_B;
            //reachedEnd = true;
        }
            
        if (this.transform.position == PointB)
        {
            ObjectState = ObjectStates.MoveB_A;
            //reachedEnd = true;
        }
        
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

    void OnCollisionEnter(Collision Col)
    {
        //Debug.Log("Collided");
        //if (Col.transform.position.y - Col.gameObject.GetComponent<BoxCollider>().size.y / 2
        //          > this.transform.position.y + this.GetComponent<BoxCollider>().size.y / 2)
            if (Col.gameObject.tag == "Player")
            {
                Parenting = true;
            }
    }
    void OnCollisionExit(Collision Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            Parenting = false;
        }
    }
    void OnCollisionStay(Collision Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            if(reachedEnd && movingUpward)
            {
                mPlayer.SendMessage("SendFlying", new Vector3(0, mSpeed/2, 0));
                reachedEnd = false;
                movingUpward = false; 
            }
        }
    }
}
