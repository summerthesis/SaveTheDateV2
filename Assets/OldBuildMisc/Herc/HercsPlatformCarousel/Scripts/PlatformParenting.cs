using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.transform.parent.parent = this.gameObject.transform;
        }
    }
    void Update() {
        PlayerMovement pm = this.gameObject.GetComponentInChildren<PlayerMovement>();

        if (pm != null) {
            if (!pm.GetIsGround()) {
                pm.transform.parent.parent = null;
            }
        }
    }
}
