using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShakeAndFall : MonoBehaviour
{
    private GameObject mPlayer;
    private int RespawnDuration, respawnCount;
    private bool Activated, Removed;
    private DOTweenAnimation mTween;
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mTween = this.GetComponent<DOTweenAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Activated)
        {
            respawnCount++;
            if(respawnCount > RespawnDuration)
            {
                
            }

        }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == mPlayer)
        {
            if(mPlayer.transform.position.y > this.transform.position.y)
            {
                Debug.Log("Player Landed on Shake and Fall Platform");
                this.DOPlay();
                
            }
        }
    
    }
}
