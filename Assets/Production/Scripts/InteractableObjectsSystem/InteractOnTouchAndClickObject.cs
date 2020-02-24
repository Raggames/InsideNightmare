using UnityEngine;

namespace Production.Scripts.Entities
{
    [CreateAssetMenu(fileName = "InteractOnTouchAndClickObject", menuName = "InteractableObjects/InteractOnTouchAndClick")]
    public class InteractOnTouchAndClickObject : InteractableObjects
    {
        public override void Initialize(GameObject container)
        {
            ContainerGameObject = container;
            InteractionIsDone = false;
            IsSeen = false;
        }

        public override void OnObjectIsSeen(GameObject go)
        {
            if (go == ContainerGameObject && InteractionIsDone == false)
            {
                IsSeen = true;
            }
        }
        public override void OnObjectIsUnseen(GameObject go)
        {
            if (go == ContainerGameObject && InteractionIsDone == false)
            {
                IsSeen = false;
            }
        }

        public override void OnObjectIsInteracted(GameObject go)
        {
            // Traitement du feedback etc
            if (go == ContainerGameObject)
            {
                InteractionIsDone = true;
                Debug.Log("Launch Interaction on " + go.name);
            }
        }
    }
}