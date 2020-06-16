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
            Instance.Enable();
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

    private void OnEnable()
    {
        Instance.Enable();
    }

    private void OnDisable()
    {
        Instance.Disable();
    }
}
