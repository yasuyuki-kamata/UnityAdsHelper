using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace UnityAdsHelper
{
    [RequireComponent(typeof(UnityAdsHelper))]
    public class UnityAdsListener : MonoBehaviour, IUnityAdsListener
    {
        [SerializeField] private UnityEvent onAdsReady;
        [SerializeField] private UnityEvent onAdsDidError;
        [SerializeField] private UnityEvent onAdsDidStart;
        [SerializeField] private UnityEvent onAdsFinished;
        [SerializeField] private UnityEvent onAdsSkipped;
        [SerializeField] private UnityEvent onAdsFailed;
        
        public void OnUnityAdsReady(string placementId)
        {
            onAdsReady.Invoke();
        }

        public void OnUnityAdsDidError(string message)
        {
            onAdsDidError.Invoke();
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            onAdsDidStart.Invoke();
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Finished:
                    onAdsFinished.Invoke();
                    break;
                case ShowResult.Skipped:
                    onAdsSkipped.Invoke();
                    break;
                case ShowResult.Failed:
                    onAdsFailed.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
            }
        }
    }
}
