using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private bool showGizmos = true;

    public Transform obj;

    public CameraPrefs prefs;
    [SerializeField] private Camera thisCamera;

    [ContextMenu("Update Camera Ref")]
    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
        Debug.Assert(thisCamera != null);
    }

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

    void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.color = Color.red;

        float minX = prefs.followX ? prefs.minX : transform.position.x;
        float maxX = prefs.followX ? prefs.maxX : transform.position.x;
        float minY = prefs.followY ? prefs.minY : transform.position.y;
        float maxY = prefs.followY ? prefs.maxY : transform.position.y;


        Vector3 lb = new Vector3(minX, minY, 0);
        Vector3 rb = new Vector3(maxX, minY, 0);
        Vector3 lt = new Vector3(minX, maxY, 0);
        Vector3 rt = new Vector3(maxX, maxY, 0);

        Gizmos.DrawLine(lb, rb);
        Gizmos.DrawLine(lt, rt);
        Gizmos.DrawLine(lb, lt);
        Gizmos.DrawLine(rb, rt);

        Gizmos.color = Color.green;

        Vector3 clb = thisCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 crb = thisCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector3 clt = thisCamera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 crt = thisCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Gizmos.DrawSphere(clb, 0.1f);
        Gizmos.DrawSphere(crb, 0.1f);
        Gizmos.DrawSphere(clt, 0.1f);
        Gizmos.DrawSphere(crt, 0.1f);

        Vector3 centerToClb = clb - transform.position;
        Vector3 centerToCrb = crb - transform.position;
        Vector3 centerToClt = clt - transform.position;
        Vector3 centerToCrt = crt - transform.position;

        Gizmos.color = Color.blue;

        Vector3 cmlb = lb + centerToClb;
        Vector3 cmrb = rb + centerToCrb;
        Vector3 cmlt = lt + centerToClt;
        Vector3 cmrt = rt + centerToCrt;

        Gizmos.DrawSphere(cmlb, 0.1f);
        Gizmos.DrawSphere(cmrb, 0.1f);
        Gizmos.DrawSphere(cmlt, 0.1f);
        Gizmos.DrawSphere(cmrt, 0.1f);



    }
}
