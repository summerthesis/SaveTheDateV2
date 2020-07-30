using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : MonoBehaviour
{
    public LocalCameraTransform startCameraTransform;

    private void Awake()
    {
        FindObjectOfType<MainCameraController>().ChangeView(startCameraTransform.position,
            Quaternion.Euler(startCameraTransform.rotation), true);
    }

}
