using Production.Plugins.RyanScriptableObjects.SOEvents.VoidEvents;
using UnityEngine;

namespace Production.Scripts.Entities
{
    public class InteractableObjectComponent : MonoBehaviour
    {
        public bool InteractionIsDone;
        public bool IsSeen;

        public bool IsActive;
        
        public InteractableObjects interactableObject;
        public VoidEvent interactionIsDoneEvent;
        private void Awake()
        {
            interactableObject.Initialize(gameObject);
        }

        public void Interact(GameObject go)
        {
            if (IsActive)
            {
                interactableObject.OnObjectIsInteracted(go);
                Debug.Log("Interact");
            }
            
        }

        public void OnSee(GameObject go)
        {
            if (IsActive)
            {
                interactableObject.OnObjectIsSeen(go);
                Debug.Log("See");
            }
           
        }

        public void OnUnSee(GameObject go)
        {
            if (IsActive)
            {
                interactableObject.OnObjectIsUnseen(go);
                Debug.Log("UnSee");
            }
            

        }

        private void Update()
        {
            if (IsActive)
            {
                InteractionIsDone = interactableObject.InteractionIsDone;
                IsSeen = interactableObject.IsSeen;
            }
            
        }
    }
}