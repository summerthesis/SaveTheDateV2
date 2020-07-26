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
    private float speed =0.05f;
    [FMODUnity.EventRef]
    public string DeathSound = "event:/Characters/Player/Health/Rewind";
    FMOD.Studio.EventInstance DeathSoundEvent;
    
    private bool triggerDeathSound;
    private bool deathEventFinished;
    private int deathEventCount;
    void Awake()
    {
        
    }
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mDeathTransform = GameObject.Find("DeathTransform");
        RecordedTransforms.Add(mDeathTransform.transform.position);
        DeathSoundEvent = FMODUnity.RuntimeManager.CreateInstance(DeathSound);
        
    }
    public void AddTransform()
    {
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
     
        if(isDead && !deathEventFinished)
        {
            if (deathEventCount == 5)
            {
                PlaySoundOneShot("event:/Characters/Player/Health/Death");
            }
            if (deathEventCount > 60) deathEventFinished = true;
            deathEventCount++;
            return;
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
                        deathEventFinished = false;
                        deathEventCount = 0;
                        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        mPlayer.GetComponent<BoxCollider>().enabled = true;
                        RecordedTransforms.Add(mDeathTransform.transform.position);

                        // START by Shu Deng (Mike)
                        FindObjectOfType<LifeCountHUDController>().LostLife();
                        FindObjectOfType<MainCameraController>().ChangeView(
                            GameManager.CurrentCheckpoint.GetComponent<CheckpointObject>().CameraTransform, false);
                        // END by Shu Deng (Mike)
                    }
                }
           
            }
        }

        if (!triggerDeathSound && isDead)
        {
            DeathSoundEvent.setParameterByName("Respawning", 0.0f, true);
            DeathSoundEvent.start();
            Debug.Log("Death rewind started");
            triggerDeathSound = true;
        }
        if(triggerDeathSound && !isDead)
        {
            DeathSoundEvent.setParameterByName("Respawning", 1.0f, true);
            triggerDeathSound = false;
            Debug.Log("Ended death rewind");
        }
    }
    
   
    void PlaySoundOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
