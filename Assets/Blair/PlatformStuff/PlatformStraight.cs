using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStraight : MonoBehaviour
{
    private float  SlowedSpeed, FastSpeed;
    public float mSpeed, NormalSpeed;
    private bool Despawn, Parenting;
    private GameObject mPlayer;
    public GameObject Despawner;
    Tween mTween;

    void Start()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        SlowedSpeed = NormalSpeed / 2;
        FastSpeed = NormalSpeed * 2;
        mSpeed = NormalSpeed;
        mTween = transform.DOPunchScale(new Vector3(.25f,.25f,.25f), 3, 5, 1);
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

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
        {
            float step = mSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Despawner.transform.position, step);
        }   

      
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
        float step = mSpeed * 120 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Despawner.transform.position, step);
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
