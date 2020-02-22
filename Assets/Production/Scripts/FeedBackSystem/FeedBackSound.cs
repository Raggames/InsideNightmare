using UnityEngine;

namespace Production.Scripts.Components
{
    [CreateAssetMenu(fileName = ("SoundFeedBack"), menuName = ("FeedBacks/SoundFeedBack"))]
    public class FeedBackSound : AbstractFeedBack
    {
        //Require Sound SetUp on FeedBackComponent GameObject in Scene
        public override void ActivateFeedBack(FeedBackComponent feedBack)
        {
            feedBack.GetComponent<AudioSource>().Play();
        }

        public override void DeactivateFeedBack(FeedBackComponent feedBack)
        {
            feedBack.GetComponent<AudioSource>().Stop();
            
        }

        public override void Initialize(FeedBackComponent feedBack)
        {
            FeedBackComponent = feedBack;
        }
    }
}