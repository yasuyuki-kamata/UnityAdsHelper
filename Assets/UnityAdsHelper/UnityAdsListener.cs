using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace UnityAdsHelper
{
    [CreateAssetMenu(fileName = "UnityAdsListener", menuName = "UnityAdsHelper/Create Listener File", order = 1001)]
    public class UnityAdsListener : ScriptableObject, IUnityAdsListener
    {
        [SerializeField] private UnityEvent onAdsReady;
        [SerializeField] private UnityEvent onAdsError;
        [SerializeField] private UnityEvent onAdsStart;
        [SerializeField] private UnityEvent onAdsFinished;
        [SerializeField] private UnityEvent onAdsSkipped;
        [SerializeField] private UnityEvent onAdsFailed;
        
        public void OnUnityAdsReady(string placementId)
        {
            onAdsReady.Invoke();
        }

        public void OnUnityAdsDidError(string message)
        {
            onAdsError.Invoke();
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            onAdsStart.Invoke();
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
