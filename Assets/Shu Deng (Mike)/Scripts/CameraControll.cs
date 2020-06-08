using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public bool moveCamera = true;
    public float smoothing = 7f;
    public Vector3 offset = new Vector3(0f, 1.5f, 0f);
    public Transform playerPosition;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        // If the camera shouldn't move, do nothing.
        if (!moveCamera)
            yield break;

        // Wait a single frame to ensure all other Starts are called first.
        yield return null;

        // Set the rotation of the camera to look at the player's position with a given offset.
        transform.rotation = Quaternion.LookRotation(playerPosition.position - transform.position + offset);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!moveCamera)
            return;

        // Find a new rotation aimed at the player's position with a given offset.
        Quaternion newRotation = Quaternion.LookRotation(playerPosition.position - transform.position + offset);

        // Spherically interpolate between the camera's current rotation and the new rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * smoothing);
    }
}
