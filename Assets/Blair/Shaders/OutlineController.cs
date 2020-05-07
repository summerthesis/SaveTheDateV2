using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private MeshRenderer mRenderer;
    public float maxOutlineWidth;
    public Color OutlineColor;
    // Start is called before the first frame update
    void Start()
    {
        mRenderer = this.GetComponent<MeshRenderer>();      
    }

    public void ShowOutline()
    {
        mRenderer.material.SetFloat("_Outline", maxOutlineWidth);
        mRenderer.material.SetColor("_OutlineColor", OutlineColor);
    }
    public void HideOutline()
    {
        mRenderer.material.SetFloat("_Outline", 0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
