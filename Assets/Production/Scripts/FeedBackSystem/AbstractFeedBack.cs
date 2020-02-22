using UnityEngine;

namespace Production.Scripts.Components
{
    public abstract class AbstractFeedBack : ScriptableObject
    {
        public FeedBackComponent FeedBackComponent;
        public abstract void ActivateFeedBack(FeedBackComponent feedBack);
        public abstract void DeactivateFeedBack(FeedBackComponent feedBack);
        public abstract void Initialize(FeedBackComponent feedBack);
    }
}