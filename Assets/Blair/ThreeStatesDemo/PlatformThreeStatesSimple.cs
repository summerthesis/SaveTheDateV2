using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformThreeStatesSimple : MonoBehaviour
{
    public GameObject oPresent, oFuture, oPast;
    public Vector3 tPresent, tFuture, tPast;
    public int mTimeState, count;
    public float mTime;
    public bool bMove;
    public Material OpaqueMat, TransparentMat;
    private Renderer mRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mRenderer = this.GetComponent<Renderer>();
        mTime = Time.time;
        tPresent = oPresent.transform.position;
        tFuture = oFuture.transform.position;
        tPast = oPast.transform.position;
        mTimeState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (count >= 180) 
            if (mTimeState <= 1) { mTimeState++; count = 0 ;mTime = Time.time; bMove = true; }
                else { mTimeState = 0; count = 0; mTime = Time.time; bMove = true; }

        if (!bMove) { count++; }


        if (bMove)
        {
            if (mRenderer.material != TransparentMat) mRenderer.material = TransparentMat;
        }
        if(bMove)
        switch(mTimeState)
        {
            case 0:
                    if (Time.time - mTime >= 1) { bMove = false; mRenderer.material = OpaqueMat; }
                    this.transform.position = Vector3.Lerp(tFuture, tPast, Time.time - mTime);
                
                break;
            
            case 1:
                    if (Time.time - mTime >= 1) { bMove = false; mRenderer.material = OpaqueMat; }
                    this.transform.position = Vector3.Lerp(tPast, tPresent, Time.time - mTime);

                break;

            case 2:
                    if (Time.time - mTime >= 1) { bMove = false; mRenderer.material = OpaqueMat; }
                    this.transform.position = Vector3.Lerp(tPresent, tFuture, Time.time - mTime);

                break;
        }
    }
}
