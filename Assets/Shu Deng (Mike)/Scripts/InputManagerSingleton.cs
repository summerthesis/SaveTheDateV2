using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerSingleton : MonoBehaviour
{
    public static PlayerInputAction Instance { get; private set; }
   
    void Awake()
    {
        if (Instance == null)
        {
            Instance = new PlayerInputAction();
            Instance.PlayerControls.Enable();
            Instance.TimeControls.Enable();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
