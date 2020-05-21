using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayer : MonoBehaviour
{
    private GameObject mPlayer;
    private bool Parenting;
    // Start is called before the first frame update
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
       if(Parenting)
        {
            mPlayer.transform.position += 
                Vector3.left * Time.deltaTime * this.GetComponent<PlatformStraight>().mSpeed;
        }
    }
    void Update()
    {
     
                
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
