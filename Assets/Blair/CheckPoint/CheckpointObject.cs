using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour
{
    private GameObject mPlayer;
    public GameObject FirstImage, RespawnPosition;
    private GameObject[] checkpoints;
    // START by Shu Deng (Mike)
    public LocalCameraTransform CameraTransform;
    // END by Shu Deng (Mike)

    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    void Update()
    {
     
    }
    void UnCheckAll()
    {
       for(int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].GetComponent<CheckpointObject>().FirstImage.gameObject.SetActive(true);
            checkpoints[i].tag = "Checkpoint";

        }
    }
    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Player")
        {
            if(this.gameObject.tag != "CurrentCheckpoint")
            {
                Debug.Log("CheckPointActivated");
                UnCheckAll();
                FirstImage.gameObject.SetActive(false);
                
                this.gameObject.tag = "CurrentCheckpoint";
                mPlayer.GetComponent<DeathController>().RecordedTransforms.Clear();
                mPlayer.GetComponent<DeathController>().RecordedTransforms.Add(RespawnPosition.transform.position);

                // START by Shu Deng (Mike)
                GameManager.CurrentCheckpoint = this.gameObject;
                Pickup.ResetDestroyedPickupList();
                // END by Shu Deng (Mike)
            }
        }
    }
}
