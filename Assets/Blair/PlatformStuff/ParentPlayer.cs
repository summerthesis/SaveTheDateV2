using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayer : MonoBehaviour
{
    private GameObject mPlayer;
    private bool Parenting;
    private float mSpeed;
    private Vector3 mPrevPos, mCurrentPos;
    public float xx, yy, zz;//the change in
    
    void Start()
    {
        //mSpeed = this.GetComponent<PlatformStraight>().mSpeed;
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
      
    }
    void Update()
    {
        mCurrentPos = this.transform.position;
        xx = mCurrentPos.x - mPrevPos.x;
        yy = mCurrentPos.y - mPrevPos.y;
        zz = mCurrentPos.z - mPrevPos.z;
        mPrevPos = mCurrentPos;
        if (Parenting)
        {
            mPlayer.transform.position += new Vector3(xx, yy, zz);
        }
      

    }

    void OnCollisionEnter(Collision Col)
    {
        if(Col.transform.position.y - Col.gameObject.GetComponent<BoxCollider>().size.y/2 
                  > this.transform.position.y + this.GetComponent<BoxCollider>().size.y/2)
        if(Col.gameObject.tag == "Player")
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
