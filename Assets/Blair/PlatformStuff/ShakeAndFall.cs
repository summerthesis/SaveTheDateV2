using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShakeAndFall : MonoBehaviour
{
    private GameObject mPlayer;
    private Vector3 mOriginalPosition;
    public int RespawnDuration;
    private int respawnCount, removedCount;
    private bool Activated, Removed;
    private DOTweenAnimation mTween;
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mTween = this.GetComponent<DOTweenAnimation>();
        mOriginalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Removed)
        {
            removedCount++;
            if(removedCount > 4 * 60 *Time.deltaTime)
            {
                mTween.DORewind();
                Removed = false;
                removedCount = 0;
            }
        }
        if(Activated)
        {
            respawnCount++;
            if(respawnCount > RespawnDuration)
            {
                this.transform.DOMoveY(mOriginalPosition.y, 4, false);
                respawnCount = 0;
                Activated = false;
                Removed = true;
            }

        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == mPlayer)
        {
        if(mPlayer.transform.position.y > this.transform.position.y)
           {

                mTween.DOPlay();
                      
           }
        }
    
    }
    void CompletedAnimation()
    {
        Activated = true;
    }
}
