using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformA_B_Commented : MonoBehaviour
{
    //This will be our standard template for every script. 
    //Im sure its not the best way in the world. But its the way were going to use, and is not up for debate.
   
    //No Comments on final versions; If your code needs comments to be understood, you need to rework it
    //before 'handing it in'.
    //During your testing //TODO: etc is fine of course.
    //This is commented only for Demo Purposes.

    //Do not Serialize any variables. I will do that as the level designer needs.
    //Name your variables completely, even temporary ones, no n = vector3.... use instead NewPosition =
    
    //Use Public variables to test, but you should be able to keep almost everything private.
    //If your messing with another scripts variables, you need to reconsider your approach. 
    //Instead Send a message to the object and code the behaviour there.
    
    public GameObject B;//The blue sphere the designer can use to place to set where to move.
    private Vector3 PointA, PointB;//Position A = the cubes start position, PositionB = B->position.
    public float StopSpeed, NormalSpeed;//Speed Settings // to be serialized
    private float mSpeed, SlowedSpeed, FastSpeed;
    public int IdleDuration; // To be serialized
    private int IdleCount;

    //Unless not needed at all, I want to see simple state machines, Using Enums. As Below:
    private enum ObjectStates
    {
        Unavailable,
        MoveA_B,
        Idling,
        MoveB_A,
        CustomEvent
    };
    ObjectStates ObjectState;//Our State Variable.
    
    //Use start or awake, I don't care, whatever works, Awake happens first.
    void Start()
    {
        //Do not use Any obscure operators. 
        //Use the basics: + - = == * / % && || and thats pretty much it. 
        //Do not overload anything. Create a function.
        SlowedSpeed = NormalSpeed / 2;
        FastSpeed = NormalSpeed * 2;
        StopSpeed = 0;//This will be custom for certain objects, but not this one. Set it to 0.
        //Do not be afraid to hard code things.
        //Celeste was made with almost 100% hardcoding.
        PointA = this.transform.position;
        PointB = B.transform.position;
        Destroy(B); //Destroy the sphere, we don't need it anymore.
        ObjectState = ObjectStates.MoveA_B;

        mSpeed = NormalSpeed;

    }

    void Update()
    {
        //our very basic, easy to understand state machine.
        switch (ObjectState)
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
                if (IdleCount <= 0) ChangeDirection();
                break;

            case ObjectStates.CustomEvent:

                break;

            default:
                break;
        }

    }

    //Use lots of functions, to see a functions definition highlight it and press F12.
    //This keeps update very simple to read and follow the logic. 
    //It also allows u to focus on one function at a time without clutter.
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
        //Think of simple elegant solutions like this one. its only 2 lines of code,
        //but in the update you understand exactly what is happening if u use it as a function instead. 
        if (this.transform.position == PointA) ObjectState = ObjectStates.MoveA_B;
        //else (either way, whatevs)
        if (this.transform.position == PointB) ObjectState = ObjectStates.MoveB_A;
    }
    //I want 1 way communication only. There will be times when we need 2 way....but not really.
    //The Boss Tells the employee what to do, The employee knows how to do it. 
    //or
    //The TimeController tells the platforms what job to do, The platforms know how to do it.
    //The TimeController tells them when to stop(if thats even needed).
    //Thats it, no accessing components, no changing remote variables etc.

    //Below are the messages to be received from the controller. 
    //If you haven't used sendmessage, see the time controller for example.
    void TimeSlow()
    {
        mSpeed = SlowedSpeed;//simple simple simple
    }
    void TimeStop()
    {
        mSpeed = StopSpeed;
    }
    void TimeFastForward()
    {
        mSpeed = FastSpeed;
    }
    void JumpForward()
    {   
        //This function can be refactored, to take in which way its going,
        //but the scope of the task doesn't require it. we don't have to be perfect we just need
        //to make a videogame - ezpz.
        if (ObjectState == ObjectStates.MoveA_B)
        {   //TLDR: Get the position where I would move to if i jumptime forward;
            //if that distance overshoots my waypoint, move to the waypoint if not, jump.
            Vector3 NewPosition =
            transform.position + transform.TransformDirection(GetLocalDirection(transform, PointB));
            if (Vector3.Distance(transform.position, NewPosition) > Vector3.Distance(transform.position, PointB))
            {
                transform.position = PointB;
            }
            else
                transform.Translate(GetLocalDirection(transform, PointB));
           
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
                transform.Translate(GetLocalDirection(transform, PointA));
        }
    }
    void RestoreToNormal()
    {
        mSpeed = NormalSpeed;
    }
    Vector3 GetLocalDirection(Transform transform, Vector3 destination)
    {
        //straight from google.
        return transform.InverseTransformDirection((destination - transform.position).normalized);
    }

}

