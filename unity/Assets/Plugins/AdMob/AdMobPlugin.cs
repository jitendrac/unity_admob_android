using System;
using UnityEngine;

public sealed class AdMobPlugin : MonoBehaviour {

	private const string CLASS_NAME = "com.nabrozidhs.admob.AdMob";

	private const string CALL_SHOW_BANNER = "showBanner";
	private const string CALL_HIDE_BANNER = "hideBanner";
	private const string CALL_SHOW_INSTERTITIAL = "showInterstitial";
	private const string CALL_REQUEST_AD = "requestAd";
	private const string CALL_REQUEST_INTERSTITIAL = "requestInterstitial";

	public enum AdSize {BANNER, MEDIUM_RECTANGLE, FULL_BANNER, LEADERBOARD, SMART_BANNER};

	public static event Action AdLoaded = delegate{};
	public static event Action InterstitialLoaded = delegate{};

#if UNITY_ANDROID
	private AndroidJavaObject plugin;
#endif

	/// <summary>
	/// Bind this instance.
	/// </summary>
	public void CreateBanner(string adUnitId, AdSize adSize, bool isTopPosition, string interstitialId = "") {
#if UNITY_ANDROID
		plugin = new AndroidJavaObject(
			CLASS_NAME,
			new AndroidJavaClass("com.unity3d.player.UnityPlayer")
				.GetStatic<AndroidJavaObject>("currentActivity"),
			adUnitId,
			adSize.ToString(),
			isTopPosition,
			interstitialId,
			gameObject.name);
#endif
	}

	/// <summary>
	/// Requests a banner ad. This method should be called
	/// after we have created a banner.
	/// </summary>
	public void RequestAd() {
#if UNITY_ANDROID
		if (plugin != null) {
			plugin.Call(CALL_REQUEST_AD, new object[0]);
		}
#endif
	}
	
	/// <summary>
	/// Requests an interstitial ad.
	/// </summary>
	public void RequestInterstitial() {
#if UNITY_ANDROID
		if (plugin != null) {
			plugin.Call(CALL_REQUEST_INTERSTITIAL, new object[0]);
		}
#endif
	}

	/// <summary>
	/// Shows the banner to the user.
	/// </summary>
	public void ShowBanner() {
#if UNITY_ANDROID
		if (plugin != null) {
			plugin.Call(CALL_SHOW_BANNER, new object[0]);
		}
#endif
	}

	/// <summary>
	/// Hides the banner from the user.
	/// </summary>
	public void HideBanner() {
#if UNITY_ANDROID
		if (plugin != null) {
			plugin.Call(CALL_HIDE_BANNER, new object[0]);
		}
#endif
	}

	/// <summary>
	/// Shows the interstitial ad to the user.
	/// </summary>
	public void ShowInterstitial() {
#if UNITY_ANDROID
		if (plugin != null) {
			plugin.Call(CALL_SHOW_INSTERTITIAL, new object[0]);
		}
#endif
	}

	public void OnAdLoaded() {
		AdLoaded();
	}

	public void OnInterstitialLoaded() {
		InterstitialLoaded();
	}
}
