using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollision : MonoBehaviour
{
    private GameObject mPlayer;
    // Start is called before the first frame update
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision Col)
    {
        if(Col.gameObject.tag == "Player")
        {
            mPlayer.GetComponent<DeathController>().isDead = true;
        }

    }
}
