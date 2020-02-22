using UnityEngine;

namespace Production.Scripts.Components
{
    [CreateAssetMenu(fileName = ("AnimationFeedBack"), menuName = ("FeedBacks/AnimationFeedBack"))]
    public class FeedBackAnimation : AbstractFeedBack
    {
        public override void ActivateFeedBack(FeedBackComponent feedBack)
        {
            feedBack.GetComponent<Animator>().SetBool("Start", true); // Start should be the name of bool to activate an animation automatic feedback pattern 
            Debug.Log("playing Animation FeedBack");
        }

        public override void DeactivateFeedBack(FeedBackComponent feedBack)
        {
            feedBack.GetComponent<Animator>().SetBool("Start", false);
        }

        public override void Initialize(FeedBackComponent feedBack)
        {
            FeedBackComponent = feedBack;
        }
    }
}