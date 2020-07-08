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
    public bool Parenting;
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
        
        mCurrentPos = transform.position;
        xx = mCurrentPos.x - mPrevPos.x;
        yy = mCurrentPos.y - mPrevPos.y;
        zz = mCurrentPos.z - mPrevPos.z;
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
        if(Parenting)
        {
            mPlayer.transform.position += new Vector3(xx, yy, zz);
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
}
