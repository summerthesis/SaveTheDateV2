using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCinematic : MonoBehaviour
{
    public GameObject CinematicPrefab;

    private PlayerInputAction m_playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        m_playerInput = InputManagerSingleton.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnInteract()
    {
        Instantiate(CinematicPrefab, transform);
        m_playerInput.PlayerControls.Disable();
    }
}
