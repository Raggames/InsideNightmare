using Production.Scripts.Entities;
using UnityEngine;

namespace Production.Scripts.CustomVRFunctions
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class CustomInteractable : MonoBehaviour
    {
        public bool IsInteracted;
        public Rigidbody rb;
        public Collider col;
        public InteractableObjectComponent InteractableObjectComponent;

        private void Awake()
        {
            gameObject.layer = 14;
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            InteractableObjectComponent = GetComponent<InteractableObjectComponent>();
        }
      
    }
}