using UnityEngine;

public class MessageView : MonoBehaviour
{
	[SerializeField] private AwardMessage _awardMessage;
	[SerializeField] private RewardManager _rewardManager;

	private void OnEnable()
	{
		_rewardManager.CoinsAwarded += CoinsAwardedHandler;
	}

	private void CoinsAwardedHandler(int coins)
	{
		_awardMessage.ShowAwardMessage(coins);
	}

	private void OnDisable()
	{
		_rewardManager.CoinsAwarded -= CoinsAwardedHandler;
	}
}
