using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    public Transform obj;

    public CameraPrefs prefs;

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.Clamp(obj.position.x, prefs.minX, prefs.maxX);
        float newY = Mathf.Clamp(obj.position.y, prefs.minY, prefs.maxY);

        transform.position = new Vector3(
            prefs.followX ? newX : transform.position.x, 
            prefs.followY ? newY : transform.position.y, 
            transform.position.z
        );
    }
}
