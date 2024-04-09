using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
	[Header("Firebase")]
	[SerializeField] private DependencyStatus _dependencyStatus;
	private DatabaseReference _DBReference;

	private void Awake()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			_dependencyStatus = task.Result;

			if (_dependencyStatus == DependencyStatus.Available)
			{
				InitializeFirebase();
				LoadFirstActiveAppUrl();
			}
			else
			{
				Debug.LogError("Could not resolve all Firebase dependencies: " + _dependencyStatus);
			}
		});
	}

	private void InitializeFirebase()
	{
		_DBReference = FirebaseDatabase.DefaultInstance.RootReference;
	}

	public void LoadFirstActiveAppUrl()
	{
		_DBReference.Child("Apps").GetValueAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogWarning($"Failed to load app data with {task.Exception}");
				return;
			}

			DataSnapshot snapshot = task.Result;

			foreach (DataSnapshot childSnapshot in snapshot.Children)
			{
				bool isActive = bool.Parse(childSnapshot.Child("IsActive").Value.ToString());

				if (isActive)
				{
					var url = childSnapshot.Child("URL").Value.ToString();
					Debug.Log("LOADING URL: " + url);
					WebViewManager.Instance.TryLoadWebViewPage(url);
					return;
				}
			}
		});
	}
}
