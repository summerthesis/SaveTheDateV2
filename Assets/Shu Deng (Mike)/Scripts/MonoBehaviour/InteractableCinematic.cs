using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableCinematic : MonoBehaviour
{
    public GameObject cinematicPrefab;
    [TextArea]
    public string cinematicText = "";
    public Transform cameraMoveTarget;
    public float cameraTargetDistance;

    private Cinematic m_Cinematic;

    // Start is called before the first frame update
    void Awake()
    {
        // Accounting for the resolution
        if (Camera.main.scaledPixelWidth / Camera.main.scaledPixelHeight < 1.777778f)  // 16:9
        {
            Vector3 displacement = (1 - 16f * Camera.main.scaledPixelHeight / (9f * Camera.main.scaledPixelWidth))
                * cameraTargetDistance * cameraMoveTarget.forward;
            cameraMoveTarget.position += displacement;
        }
    }

    void OnInteract()
    {
        m_Cinematic = Instantiate(cinematicPrefab).GetComponent<Cinematic>();  //Calls the Awake() immediately 
        if (cinematicText != "")
        {
            m_Cinematic.textContent = cinematicText;
        }
        m_Cinematic.cameraTargetTransform = cameraMoveTarget;
    }
}
