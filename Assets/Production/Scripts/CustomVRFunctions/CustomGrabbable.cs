using UnityEngine;

namespace Production.Scripts.CustomVRFunctions
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class CustomGrabbable : MonoBehaviour
    {
        public bool IsGrabbed;
        public Rigidbody rb;
        public Collider col;
        

        private void Awake()
        {
            gameObject.layer = 10;
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
        }
        
    }
}