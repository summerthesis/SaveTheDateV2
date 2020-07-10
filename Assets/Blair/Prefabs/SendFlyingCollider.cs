using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFlyingCollider : MonoBehaviour
{
    private GameObject mParent, mPlayer;
    public Vector3 mAngle, mStartPos, mStartRot;
    private float xRot, yRot, zRot;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, mAngle);
    }
    void Start()
    {
        mStartPos = this.transform.position;
        xRot = this.transform.eulerAngles.x;
        yRot = this.transform.eulerAngles.y;
        zRot = this.transform.eulerAngles.z;
        mStartRot = new Vector3(xRot, yRot, zRot);
        mParent = this.transform.parent.gameObject;
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

  
    // Update is called once per frame
    void Update()
    {
        this.transform.position = mStartPos;
        this.transform.eulerAngles = mStartRot;
    }
    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(mParent.GetComponent<GiantGearScript>().mSpeed ==
               mParent.GetComponent<GiantGearScript>().FastSpeed)
            mPlayer.SendMessage("SendFlying", mAngle);
        }
    }
}
