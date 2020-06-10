using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCinematic : MonoBehaviour
{
    public GameObject CinematicPrefab;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnInteract()
    {
        Instantiate(CinematicPrefab, transform);        
    }
}
