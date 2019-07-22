using System;
using UnityEngine;

namespace UnityAdsHelper
{
    [CreateAssetMenu(fileName = "UnityAdsSettings", menuName = "UnityAdsHelper/Create Settings File", order = 1000)]
    public class UnityAdsSettings : ScriptableObject
    {
        public string gameIdAppleAppStore = "2942932";
        public string gameIdGooglePlay = "2942930";
        public bool isTestMode = false;
        public string videoPlacementId = "video";
        public string rewardedVideoPlacementId = "rewardedVideo";
    }
}