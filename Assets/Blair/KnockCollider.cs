using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockCollider : MonoBehaviour
{
    public GameObject ParentWithTimeScript, mPlayer;
    private Component timeScript;
    [System.Serializable]
    public enum ObstacleType { Pendulum, MoveAtoB, StraightMoving }
    public ObstacleType mType;
    private int knockCount;
    private bool isKnocking;
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
     if(isKnocking)
        {
            knockCount++;
            if(knockCount > 30)
            {
                isKnocking = false;
                //CancelFlying();
                knockCount = 0;
                mPlayer.GetComponent<DeathController>().isDead = true;
            }
        }
    }
    void CancelFlying()
    {
        mPlayer.SendMessage("SendFlying", Vector3.zero);
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == mPlayer)
        {
            isKnocking = true;
            Debug.Log("Test Collision Knockback");
            Vector3 Dir = mPlayer.transform.forward * -35;
            Dir.y = 10;
            mPlayer.SendMessage("SendFlying", Dir.normalized * 25);
        }
    }
 
}
