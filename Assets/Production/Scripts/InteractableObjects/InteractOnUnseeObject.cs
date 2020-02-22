using UnityEngine;

namespace Production.Scripts.Entities
{
   
    [CreateAssetMenu(fileName = "InteractOnUnseenObject", menuName = "InteractableObjects/OnUnseen")]
    public class InteractOnUnseeObject : InteractableObjects
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
                go.GetComponent<InteractableObjectComponent>().Interact(go);

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