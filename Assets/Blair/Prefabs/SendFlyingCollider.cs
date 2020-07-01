using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFlyingCollider : MonoBehaviour
{
    private GameObject mParent, mPlayer;
    public Vector3 mAngle;
    // Start is called before the first frame update
    void Start()
    {
        mParent = this.transform.parent.gameObject;
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollision(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            mPlayer.SendMessage("SendFlying", mAngle);
        }
    }
}
