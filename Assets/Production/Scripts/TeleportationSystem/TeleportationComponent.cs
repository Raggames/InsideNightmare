using Production.Plugins.RyanScriptableObjects.SOEvents.BoolEvents;
using Production.Scripts.Entities;
using UnityEngine;

namespace Production.Scripts.Components
{
    [RequireComponent(typeof(Collider))]
    public class TeleportationComponent : MonoBehaviour
    {
        private CharacterController characterController;
        private OVRPlayerController ovrPlayerController;
        [SerializeField] private FieldDetectorComponent _fieldDetectorComponent;
        private Collider _collider;

        public BoolEvent OnPlayerIsTeleporting;
        private void Awake()
        {
            characterController = GetComponentInParent<CharacterController>();
            ovrPlayerController = GetComponentInParent<OVRPlayerController>();
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        public void Teleport(Vector3 toDestination, Vector3 toAim = new Vector3(), Quaternion rot = new Quaternion())
        {
            OnPlayerIsTeleporting.Raise(true);
            _fieldDetectorComponent.Teleporting = true;
            ovrPlayerController.EnableLinearMovement = false;
            //ovrPlayerController.EnableRotation = false;
            characterController.enabled = false;
            characterController.transform.position = toDestination;
            
            if(toAim != new Vector3())
            {
                characterController.transform.LookAt(toAim);
            }

            if (rot != new Quaternion())
            {
                characterController.transform.rotation = rot;
            }
            
            characterController.enabled = true;
            ovrPlayerController.EnableLinearMovement = true;
            //ovrPlayerController.EnableRotation = true;
            OnPlayerIsTeleporting.Raise(false);
            _fieldDetectorComponent.Teleporting = false;
        }

        
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.layer == 13)
            {
                Debug.Log("trigger");
                var tpObj = other.GetComponent<TeleportationObject>();
                Teleport(tpObj.TeleportDestination.position, tpObj.ToAim.position);
               
            }
        }
    }
}