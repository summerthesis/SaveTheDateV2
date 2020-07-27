using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour
{
    private GameObject mPlayer;
    public GameObject FirstImage;

    // START by Shu Deng (Mike)
    public LocalCameraTransform CameraTransform;
    // END by Shu Deng (Mike)

    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
     
    }
    
    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Player")
        {
            if(this.gameObject.tag != "CurrentCheckpoint")
            {
                Debug.Log("CheckPointActivated");
                FirstImage.gameObject.SetActive(false);
                this.gameObject.tag = "CurrentCheckpoint";
                mPlayer.GetComponent<DeathController>().RecordedTransforms.Clear();
                mPlayer.GetComponent<DeathController>().AddTransform();

                // START by Shu Deng (Mike)
                GameManager.CurrentCheckpoint = this.gameObject;
                Pickup.ResetDestroyedPickupList();
                // END by Shu Deng (Mike)
            }
        }
    }
}
