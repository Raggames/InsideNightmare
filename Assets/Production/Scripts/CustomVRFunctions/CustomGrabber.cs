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
    [SerializeField] protected bool IsInteracting;
    [SerializeField] private CustomGrabbable Grabbed;
    [SerializeField] private CustomInteractable Interacted;
    [SerializeField] private List<Collider> grabbableObjects = new List<Collider>();
    [SerializeField] private List<Collider> interactableObjects = new List<Collider>();
    
    public GameObject m_player;
    
    public LayerMask Grabbable;//objet attrappable
    public LayerMask Interactable;//objet uniquement interagissable en touchant
    
    void Start()
    {
        IsGrabbing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer + " layer");
        if (other.gameObject.layer == 10)//Layer Grabbable
        {
           if(!grabbableObjects.Find(item => item == other)) grabbableObjects.Add(other);
        }

        if (other.gameObject.layer == 14)
        {
            if(!interactableObjects.Find(item => item == other)) interactableObjects.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(grabbableObjects.Find(item=> item == other)) grabbableObjects.Remove(other);
        if(interactableObjects.Find(item=> item == other)) interactableObjects.Remove(other);
    }

    void OnInteract()
    {
        Debug.Log("OnInteract");
        if (!IsInteracting && interactableObjects.Count>0)
        {
            interactableObjects.Sort(delegate(Collider collider1, Collider collider2)
            {
                return Vector3.Distance(transform.position, collider1.gameObject.transform.position)
                    .CompareTo(Vector3.Distance(transform.position, collider2.gameObject.transform.position));
                
            });
            Interacted = interactableObjects[0].GetComponent<CustomInteractable>();
            Interacted.InteractableObjectComponent.Interact(Interacted.gameObject);
            Debug.Log("Has Interact");
            IsInteracting = false;
        }
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
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller) > 0.55f)//grabBegin
        {
            OnGrab();
        }
        if (Input.GetKey(KeyCode.G))
        {
            OnGrab();
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller) < 0.30f)
        {
            OnRelease();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.X))
        {
            OnInteract();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("KeyCodeI");
            OnInteract();
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
