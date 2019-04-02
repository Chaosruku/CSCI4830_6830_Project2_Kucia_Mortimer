using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Transform head;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void teleport(Vector3 playerFeetWorldSpace, Vector3 playerDirectionWorldSpace)
    {
        Vector3 currentFeetPosTrackingSpace = this.transform.worldToLocalMatrix.MultiplyPoint(head.position);
        currentFeetPosTrackingSpace.y = 0;
        Vector3 currentFeetPosWorldSpace = this.transform.localToWorldMatrix.MultiplyPoint(currentFeetPosTrackingSpace);

        Vector3 feetOffsetWorldSpace = playerFeetWorldSpace - currentFeetPosWorldSpace;
        this.transform.Translate(feetOffsetWorldSpace, Space.World);

    }
}
