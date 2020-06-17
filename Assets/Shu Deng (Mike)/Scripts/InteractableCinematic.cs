using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCinematic : MonoBehaviour
{
    public GameObject CinematicPrefab;
    [TextArea]
    public string CinematicText = "";
    public Transform CameraMoveTarget;

    private Cinematic m_Cinematic;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnInteract()
    {
        m_Cinematic = Instantiate(CinematicPrefab, transform).GetComponent<Cinematic>();
        if (CinematicText != "")
        {
            m_Cinematic.TextContent = CinematicText;
        }
        m_Cinematic.CameraTargetTransform = CameraMoveTarget;
    }
}
