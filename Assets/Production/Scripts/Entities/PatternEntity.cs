using System.Collections.Generic;
using UnityEngine;

namespace Production.Scripts.Entities
{
    public class PatternEntity : MonoBehaviour
    {
        public List<InteractableObjectComponent> InteractableObjects = new List<InteractableObjectComponent>();
        public bool PatternIsDone;

        public void ActivatePattern()
        {
            foreach (var obj in InteractableObjects)
            {
                obj.IsActive = true;
            }
        }

        public void DesactivatePattern()
        {
            foreach (var obj in InteractableObjects)
            {
                obj.IsActive = false;
            }
        }

    }
}