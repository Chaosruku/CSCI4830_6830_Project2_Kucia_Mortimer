using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using Valve.VR;

public class hand : MonoBehaviour
{
    public Transform holder;
    public Rigidbody rb;
    private Vector3 positionHolder;
    private Quaternion rotationHolder;
    public Transform head;
    public Transform controller;
    public float linearDistance = .5f;
    public float linearGain = 2;
    public player player;
    public SteamVR_Input_Sources mySource;
    //PickupObject currentAttachment = null;
    public float pickupTriggerThreshold;
    public float releaseTriggerThreshold;
    public bool disappearOnPickup;
    TrackedPoseDriver trackedPose;
    public Laser laser;
    public float maxLaserDistance;
    // Start is called before the first frame update
    void Start()
    {
        trackedPose = GetComponent<TrackedPoseDriver>();

        positionHolder = holder.worldToLocalMatrix.MultiplyPoint(this.transform.position);
        rotationHolder = this.transform.rotation * Quaternion.Inverse(holder.rotation);

        rb.useGravity = false;
        rb.maxAngularVelocity = Mathf.Infinity;
    }

    void FixedUpdate()
    {   Vector3 desiredPos = holder.localToWorldMatrix.MultiplyPoint(positionHolder);
        Vector3 currentPos = this.transform.position;
        Quaternion desiredRot = holder.rotation * rotationHolder;
        Quaternion currentRot = this.transform.rotation;
        rb.velocity = (desiredPos - currentPos) / Time.fixedDeltaTime;

        Quaternion offsetRot = desiredRot * Quaternion.Inverse(currentRot);
        float angle; Vector3 axis;
        offsetRot.ToAngleAxis(out angle, out axis);
        Vector3 rotationDiff = angle * Mathf.Deg2Rad * axis;
        rb.angularVelocity = rotationDiff / Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headPos = head.position;
        Vector3 conPos = controller.position;
        Vector3 between = conPos - headPos;
        float dist = between.magnitude;

        //if(dist <= linearDistance)
        //{
        //    transform.position = conPos;
        //}
        //else
        //{
        //    float extra = dist - linearDistance;
        //    transform.position = headPos + between.normalized * (linearDistance + extra * linearGain);
        //}

        RaycastHit[] hits = Physics.RaycastAll(new Ray(laser.transform.position, laser.transform.forward), maxLaserDistance);

        if (hits.Length > 0)
        {
            int furthest_index = 0;
            float furthest_distance = hits[0].distance;
            for (int i = 1; i < hits.Length; i++)
            {
                if (hits[i].distance > furthest_distance)
                {
                    furthest_index = 1;
                    furthest_distance = hits[i].distance;
                }
            }

            laser.length = hits[0].distance;

            if (SteamVR_Input.GetStateDown("GrabPinch", mySource))
            {
                Rigidbody rb = hits[furthest_index].collider.attachedRigidbody;
                if (rb == null)
                {
                    Vector3 point = hits[0].point;
                    player.teleport(point, Vector3.zero);
                }
                /*
                if(rb != null)
                {
                    
                }
                else
                {
                    Vector3 point = hits[0].point;
                    player.teleport(point, Vector3.zero);
                }*/
            }
        }
        else
        {
            laser.length = maxLaserDistance;
        }



    }
}
