using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Local Camera Transform", 
    menuName = "Save The Date/Local Camera Transform")]
public class LocalCameraTransform : ScriptableObject
{
    public Vector3 position, rotation;
}
