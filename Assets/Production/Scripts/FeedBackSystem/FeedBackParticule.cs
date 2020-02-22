using UnityEngine;

namespace Production.Scripts.Components
{
    [CreateAssetMenu(fileName = ("ParticuleFeedBack"), menuName = ("FeedBacks/ParticuleFeedBack"))]
    public class FeedBackParticule : AbstractFeedBack
    {
        public override void ActivateFeedBack(FeedBackComponent feedBack)
        {
            Debug.Log("playing Particule FeedBack");
            feedBack.GetComponent<ParticleSystem>().Play();
//            ParticleSystem.Play();
        }

        public override void DeactivateFeedBack(FeedBackComponent feedBack)
        {
            feedBack.GetComponent<ParticleSystem>().Stop();
        }

        public override void Initialize(FeedBackComponent feedBack)
        {
            FeedBackComponent = feedBack;
        }
    }
}