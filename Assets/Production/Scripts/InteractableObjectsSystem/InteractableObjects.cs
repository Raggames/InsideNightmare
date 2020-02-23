using System;
using Production.Plugins.RyanScriptableObjects.SOEvents.VoidEvents;
using Production.Plugins.RyanScriptableObjects.SOReferences.GameObjectReference;
using UnityEngine;

namespace Production.Scripts.Entities
{
    public abstract class InteractableObjects : ScriptableObject
    {
        public bool IsSeen;
        public bool InteractionIsDone;
        public GameObject ContainerGameObject;
        

        public abstract void Initialize(GameObject go);
        public abstract void OnObjectIsInteracted(GameObject go);
        public abstract void OnObjectIsSeen(GameObject go);
        public abstract void OnObjectIsUnseen(GameObject go);

    }
    
}