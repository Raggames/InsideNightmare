using System;
using System.Collections.Generic;
using Production.Plugins.RyanScriptableObjects.SOEvents.VoidEvents;
using Production.Scripts.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Production.Scripts.Entities
{
    public class InteractableObjectComponent : MonoBehaviour
    {
        public bool InteractionIsDone;
        public bool IsSeen;

        public bool IsActive;
        
        public InteractableObjects interactableObject;
        public VoidEvent interactionIsDoneEvent;

        [SerializeField] private List<FeedBacks> FeedBacks = new List<FeedBacks>(); 
        private void Awake()
        {
            interactableObject.Initialize(gameObject);
           
        }

        public void Interact(GameObject go)
        {
            if (IsActive)
            {
                interactableObject.OnObjectIsInteracted(go);
                interactionIsDoneEvent.Raise();
                foreach (var feedBack in FeedBacks)
                {
                    feedBack.FeedBackComponent
                        .OnFeedBackTrigger(feedBack.FeedBackDelay); //.FeedBackType.ActivateFeedBack(feedBack.feedBackcomponent);
                }
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

    [Serializable]
    public struct FeedBacks
    {
        public float FeedBackDelay;
        public FeedBackComponent FeedBackComponent;
    }
}