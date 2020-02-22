using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.Scripts.Components
{
    public class FeedBackComponent : MonoBehaviour
    {
        public enum FeedBackDelay
        {
            Before,
            During,
            After,
        }
        public FeedBackDelay feedBackDelay;

        public AbstractFeedBack FeedBackType;

        private void Awake()
        {
            if(FeedBackType != null) FeedBackType.Initialize(this);
            else Debug.Log("FeedBack Component failed to initialize on " + gameObject.name);
        }

        public void OnFeedBackTrigger(float Delay)
        {
            
            if (feedBackDelay==FeedBackDelay.Before)
            {
                StartCoroutine(BeforeDelay(Delay));
            }
            if (feedBackDelay==FeedBackDelay.During)
            {
                StartCoroutine(DuringDelay(Delay));
            }
            if (feedBackDelay==FeedBackDelay.After)
            {
                StartCoroutine(WaitAfterDelay(Delay));
            }
        }

        IEnumerator WaitAfterDelay(float Delay)
        {
            float timer = 0;
            while (timer < Delay)
            {
               timer += Time.deltaTime;
               yield return null;
            }
            FeedBackAfterDelay();
            
        }
        IEnumerator BeforeDelay(float Delay)
        {
            FeedBackBeforeDelay();
            float timer = 0;
            while (timer < Delay)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            FeedBackStop();
        }
        IEnumerator DuringDelay(float Delay)
        {
            
            float timer = 0;
            while (timer < Delay)
            {
                timer += Time.deltaTime;
                FeedBackDuringDelay();
                yield return null;
            }
            FeedBackStop();

        }
        void FeedBackAfterDelay()
        {
            if (FeedBackType != null)
            {
                FeedBackType.ActivateFeedBack(this);
            }
            else
            {
                Debug.Log("Feed Back SO is missing");
            }
        }

        void FeedBackStop()
        {
            if (FeedBackType != null)
            {
                FeedBackType.DeactivateFeedBack(this);
            }
            else
            {
                Debug.Log("Feed Back SO is missing");
            }
        }
        void FeedBackBeforeDelay()
        {
            if (FeedBackType != null)
            {
                FeedBackType.ActivateFeedBack(this);
            }
            else
            {
                Debug.Log("Feed Back SO is missing");
            }
        }

        void FeedBackDuringDelay()
        {
            if (FeedBackType != null)
            {
                FeedBackType.ActivateFeedBack(this);
            }
            else
            {
                Debug.Log("Feed Back SO is missing");
            }
        }
    }
}