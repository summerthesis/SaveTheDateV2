using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStraight : MonoBehaviour
{
    private float  SlowedSpeed, FastSpeed;
    public float mSpeed, NormalSpeed;
    private bool Despawn;
    private GameObject mPlayer;
    Tween mTween;
    void Start()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        SlowedSpeed = mSpeed / 2;
        FastSpeed = mSpeed * 2;
        NormalSpeed = mSpeed;
        mTween = transform.DOPunchScale(new Vector3(.25f,.25f,.25f), 3, 5, 1);
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Despawn)
        {
            transform.position += Vector3.left * Time.deltaTime * mSpeed * 2;
            transform.DOScale(0, .2f);
            if (transform.localScale == Vector3.zero)
                Destroy(gameObject);

        }
        else
            transform.position += Vector3.left * Time.deltaTime * mSpeed;
    }

     void TimeSlow()
    {
        mSpeed = SlowedSpeed;
    }
    void TimeStop()
    {
        mSpeed = 0;
    }
    void TimeFastForward()
    {
        mSpeed = FastSpeed;
    }
    void JumpForward()
    {
        transform.Translate(-transform.right * 4);
    }
    void RestoreToNormal()
    {
        mSpeed = NormalSpeed;
    }
    Vector3 GetLocalDirection(Transform transform, Vector3 destination)
    {
        return transform.InverseTransformDirection((destination - transform.position).normalized);
    }
    void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.name == "Despawner")
        {
            Despawn = true;
        }
    }
}
