using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (GameManager.MainCamera.scaledPixelWidth / GameManager.MainCamera.scaledPixelHeight < 1.777778f)  // 16:9
        {
            Vector3 displacement = (1 - 16f * GameManager.MainCamera.scaledPixelHeight / (9f * GameManager.MainCamera.scaledPixelWidth))
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
