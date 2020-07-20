using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject Despawner;
    public GameObject SimplePlatformPrefab;
    public float PlatformMoveSpeed;
    public int Interval, StartDelay, startCount;
    private int Count;
    private Vector3 Forward;
    private GameObject NewPlatform;
    private bool started, finished;
    public bool IsOneShot;
    void Start()
    {
        Forward = this.transform.forward;
        if (IsOneShot) Interval = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOneShot && finished)
            return;

        if (!IsOneShot)
        {
        if (!started) startCount++;
        if (!started && startCount > StartDelay) started = true;
        }


      
        if(started)
        {
            Count++;
            if (Count >= Interval)
            {
                if (!IsOneShot) { Count = 0; } else finished = true;
                
                NewPlatform = Instantiate(SimplePlatformPrefab, this.transform.position, this.transform.rotation);
                NewPlatform.GetComponent<PlatformStraight>().mSpeed = PlatformMoveSpeed;
                NewPlatform.GetComponent<PlatformStraight>().Despawner = Despawner;
            }
        }
        
    }
    void StartMe()
    {
        started = true;
    }
}
