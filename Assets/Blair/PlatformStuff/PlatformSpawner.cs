using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject Despawner;
    public GameObject SimplePlatformPrefab;
    public float PlatformMoveSpeed;
    public int Interval;
    private int Count;
    private Vector3 Forward;
    private GameObject NewPlatform;
    void Start()
    {
        Forward = this.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Count++;
        if (Count >= Interval)
        {
            Count = 0;
            NewPlatform = Instantiate(SimplePlatformPrefab, this.transform.position, this.transform.rotation);
            NewPlatform.GetComponent<PlatformStraight>().mSpeed = PlatformMoveSpeed;
            NewPlatform.GetComponent<PlatformStraight>().Despawner = Despawner;
        }
    }
}
