using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    public bool followX = true;
    public bool followY = true;

    public float minX = -1.577112f;
    public float maxX = 0.4337616f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newX = player.position.x;
        float newY = player.position.y;

        newX = Mathf.Clamp(newX, minX, maxX);

        transform.position = new Vector3(followX ? newX : transform.position.x, followY ? newY : transform.position.y, transform.position.z);
    }
}
