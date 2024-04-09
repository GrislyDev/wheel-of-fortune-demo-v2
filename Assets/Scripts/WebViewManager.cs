using UnityEngine;
using Gpm.WebView;
using System.Collections.Generic;

public class WebViewManager : MonoBehaviour
{
	public static WebViewManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void ShowWebPage(string url)
	{
		GpmWebView.ShowUrl(url, new GpmWebViewRequest.Configuration()
		{
			style = GpmWebViewStyle.POPUP,
			orientation = GpmOrientation.UNSPECIFIED,
			isClearCookie = true,
			isClearCache = true,
			isNavigationBarVisible = true,
			isCloseButtonVisible = true,
			supportMultipleWindows = true
		},
		OnCallback,
		new List<string>());
	}

	private void OnCallback(
	GpmWebViewCallback.CallbackType callbackType,
	string data,
	GpmWebViewError error)
	{
		Debug.Log("OnCallback: " + callbackType);
	}

	public void TryLoadWebViewPage(string url)
	{
		if (PlayerPrefs.HasKey("FirstOpened"))
		{
			ShowWebPage(url);
		}
		else
		{
			PlayerPrefs.SetString("FirstOpened", "true");
		}
	}
}
