using UnityEngine;

namespace UnityAdsHelper.Example.Scripts
{
	public class CallbackExamples : MonoBehaviour
	{
		public void DoSomethingWhenAdsReady()
		{
			Debug.Log("DoSomethingWhenAdsReady");
		}

		public void DoSomethingWhenAdsDidError()
		{
			Debug.Log("DoSomethingWhenAdsDidError");
		}

		public void DoSomethingWhenAdsDidStart()
		{
			Debug.Log("DoSomethingWhenAdsDidStart");
		}

		public void DoSomethingWhenAdsFinished()
		{
			Debug.Log("DoSomethingWhenAdsFinished");
		}

		public void DoSomethingWhenAdsSkipped()
		{
			Debug.Log("DoSomethingWhenAdsSkipped");
		}

		public void DoSomethingWhenAdsFailed()
		{
			Debug.Log("DoSomethingWhenAdsFailed");
		}
	}
}