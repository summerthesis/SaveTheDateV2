using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    private GameObject mPlayer;
    private GameObject mDeathTransform;
    //[HideInInspector]
    public List<Vector3> RecordedTransforms;
    private float RecordCount;
    public bool isDead;
    private float journeyLength;
    private float startTime;
    private float speed =0.15f;
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mDeathTransform = GameObject.Find("DeathTransform");
        RecordedTransforms.Add(mDeathTransform.transform.position);
    }

    void Update()
    {
        if(!isDead)
        {
            RecordCount++;
            if (RecordCount >= 560)
            {
                RecordCount = 0;
                RecordedTransforms.Add(mDeathTransform.transform.position);
            }
        }
     
        if(isDead)
        {
            mPlayer.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            if (RecordedTransforms.Count != 0)
            {
                journeyLength = Vector3.Distance(transform.position, RecordedTransforms.Last());
                float distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp
                (transform.position, RecordedTransforms.Last(), fractionOfJourney);
               
                if(Vector3.Distance(mPlayer.transform.position,RecordedTransforms.Last()) < 1.0f)
                {
                    RecordedTransforms.RemoveAt(RecordedTransforms.Count - 1);
                    if (RecordedTransforms.Count == 0)
                    {
                        isDead = false;
                        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        mPlayer.GetComponent<BoxCollider>().enabled = true;
                        RecordedTransforms.Add(mDeathTransform.transform.position);
                    }
                }
           
            }
        }
    }
    
    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Checkpoint")
        {
            if(Col.gameObject.tag != "CurrentCheckpoint")
            {
                Col.gameObject.tag = "CurrentCheckpoint";
                
            }
        }
    }
    void OnCollisionEnter()
    {

    }
}
