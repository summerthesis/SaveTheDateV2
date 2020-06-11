using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableText : MonoBehaviour
{
    public GameObject TextUIPrefab;
    public Canvas ScreenCanvas;

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
        Instantiate(TextUIPrefab, ScreenCanvas.transform, false);
    }
}
