using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    public bool showing = false;
    public int throttleShowing = 0;
    private MeshRenderer mRenderer;
    public GameObject mPlayer;
    public float maxOutlineWidth;
    public Color OutlineColor;

    // Start is called before the first frame update
    void Start()
    {
        mRenderer = this.GetComponent<MeshRenderer>();
        mPlayer = GameObject.Find("TimePart");
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
            if (mPlayer.GetComponent<TimeInputControls>().viewedObject == this.gameObject)
            {
                if (showing == false) ShowOutline();
                showing = true;
            }
            else
            {
                if (showing == true) HideOutline();
                showing = false;
            }
        
    }
}
