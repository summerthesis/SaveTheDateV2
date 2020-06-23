using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRipples : MonoBehaviour
{
    public GameObject ripplesVFX;
    public GameObject hitVFX;

    private Material mat;

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Bullet")
        {
            var ripples = Instantiate(ripplesVFX, transform) as GameObject;
            var psr = ripples.transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
            mat = psr.material;
            mat.SetVector("_SphereCenter", co.contacts[0].point);

            Destroy(ripples, 2);

            var hit = Instantiate (hitVFX, co.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(hit, 2);
        }
    }
}
