using System.Collections;
using System.Collections.Generic;
using Production.Scripts.CustomVRFunctions;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGrabber : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;

    [SerializeField] protected bool IsGrabbing;
    
    [SerializeField] private CustomGrabbable Grabbed;
    [SerializeField] private List<Collider> grabbableObjects = new List<Collider>();
    public GameObject m_player;
    
    void Start()
    {
        IsGrabbing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)//Layer Grabbable
        {
           if(!grabbableObjects.Find(item => item == other)) grabbableObjects.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(grabbableObjects.Find(item=> item == other)) grabbableObjects.Remove(other);
    }

    void OnGrab()
    {
        if (!IsGrabbing)
        {
            grabbableObjects.Sort(delegate(Collider collider1, Collider collider2)
            {
                return Vector3.Distance(transform.position, collider1.gameObject.transform.position)
                    .CompareTo(Vector3.Distance(transform.position, collider2.gameObject.transform.position));});
            Grabbed = grabbableObjects[0].GetComponent<CustomGrabbable>();
            if (!Grabbed.IsGrabbed)
            {
                if (m_player != null)
                {
                    Collider[] playerColliders = m_player.GetComponentsInChildren<Collider>();
                    foreach (Collider pc in playerColliders)
                    {
                        Collider[] colliders = Grabbed.GetComponentsInChildren<Collider>();
                        foreach (Collider c in colliders)
                        {
                            Physics.IgnoreCollision(c, pc, true);
                        }
                    }
                }
               Grabbed.rb.isKinematic = true;
               Grabbed.rb.useGravity = false;
               Grabbed.gameObject.transform.parent = gameObject.transform;
               grabbableObjects.Clear();
               Grabbed.IsGrabbed = true;
               IsGrabbing = true;
               
            }
        }

       
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller) > 0.55f && !IsGrabbing)//grabBegin
        {
            OnGrab();
        }

        if (Input.GetKey(KeyCode.G) && !IsGrabbing)
        {
            OnGrab();
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller) < 0.30f)
        {
            OnRelease();
        }

    }

    private void OnRelease()
    {
        if (Grabbed != null)
        {
            if (m_player != null)
            {
                Collider[] playerColliders = m_player.GetComponentsInChildren<Collider>();
                foreach (Collider pc in playerColliders)
                {
                    Collider[] colliders = Grabbed.GetComponentsInChildren<Collider>();
                    foreach (Collider c in colliders)
                    {
                        Physics.IgnoreCollision(c, pc, false);
                    }
                }
            }
            Grabbed.rb.isKinematic = false;
            Grabbed.rb.useGravity = true;
            Grabbed.gameObject.transform.parent = null;
            OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(m_controller), orientation = OVRInput.GetLocalControllerRotation(m_controller) };
            OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
            Grabbed.rb.velocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(m_controller);
            Grabbed.rb.angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(m_controller);
            Grabbed.IsGrabbed = false;
            Grabbed = null;
            IsGrabbing = false;
        }
       
    }
}
